#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "node.h"

__declspec(dllexport)
float calEuclDis(Point* pt1, Point* pt2) {
	return (float)sqrt(
		pow(pt1->x - pt2->x, 2) +
		pow(pt1->y - pt2->y, 2) +
		pow(pt1->z - pt2->z, 2)
	);
}

__declspec(dllexport)
void calRelativeDis() {
  for (int i = 0; i < Row; i++) {
    for (int j = 0; j < Col; j++) {
      if (i > 0)
		  mesh.nodes[i][j].top = calEuclDis(&mesh.nodes[i][j], &mesh.nodes[i-1][j]);
	  if (i < Row - 1)
		  mesh.nodes[i][j].bottom = calEuclDis(&mesh.nodes[i][j], &mesh.nodes[i+1][j]);
      if (j > 0)
		  mesh.nodes[i][j].left = calEuclDis(&mesh.nodes[i][j], &mesh.nodes[i][j-1]);
	  if (j < Col - 1)
		  mesh.nodes[i][j].right = calEuclDis(&mesh.nodes[i][j], &mesh.nodes[i][j+1]);
    }
  }
}

double getGeodesicDis(int startRow, int startCol, int endRow, int endCol) {
	Node* headDist; // dist��¼�����������̾���
	Node* headNodeSet; // nodeSet��¼��ʱ����������
	/* 
	** ��ʼ�������б�
	*/
	Node nodeDist;
	Node nodeNodeSet;
	nodeDist.self = &mesh.nodes[startRow][startCol];
	nodeNodeSet.self = &mesh.nodes[startRow][startCol];
	headDist = &nodeDist;
	headNodeSet = &nodeNodeSet;
	while (contains(headNodeSet, &mesh.nodes[endRow][endCol])
		|| !contains(headDist, &mesh.nodes[endRow][endCol])
		|| find(headDist, &mesh.nodes[endRow][endCol])->dist > headDist->dist) {
		float cost = headNodeSet->dist;
		Point* here = headNodeSet->self;
		headNodeSet = headNodeSet->next;
		headNodeSet->previous = NULL;
		float hereDist = find(headDist, here)->dist;
		if (hereDist >= cost) {
			void* rst = getAdjacent(headNodeSet); // ��ø���ǰ������ڵĽ��
			for (int i = 0; i < ((int*)rst)[0]; i++) {
				float nextDist = ((Node*)rst)[i + 1].dist + cost;
				Point* there = ((Node*)rst)[i + 1].self;
				// ���ָ���·��ʱ������dist[]����ӵ�nodeSet

				/* ����dist�б�
				** ���dist�б���û��there�㣬������ӽ�ȥ
				** ���dist�б�����there�㣬����dist��nextDist�������dist
				*/ 
				if (contains(headDist, there)) {
					Node* thereNode = find(headDist, there);
					if (thereNode->dist > nextDist) {
						thereNode->dist = nextDist;
					}
				} else {
					Node node;
					node.dist = nextDist;
					node.self = there;
					node.next = headDist;
					headDist->previous = &node;
				}
				/* ����nodeSet�߽�㼯��
				** ��ǰ����ĩ�㣬���ٽ�������߽�㼯����
				*/
				if (there != &mesh.nodes[endRow][endCol]) {
					Node vertex;
					vertex.dist = nextDist;
					vertex.self = there;
					vertex.next = headNodeSet;
					headNodeSet->previous = &vertex;
				}
			}
		}
		sortNodeChain(headNodeSet);
	}
	return find(headDist, &mesh.nodes[endRow][endCol])->dist;
}