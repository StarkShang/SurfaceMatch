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
	Node* headDist; // dist记录到各个点的最短距离
	Node* headNodeSet; // nodeSet记录当时的搜索顶点
	/* 
	** 初始化两个列表
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
			void* rst = getAdjacent(headNodeSet); // 获得跟当前结点相邻的结点
			for (int i = 0; i < ((int*)rst)[0]; i++) {
				float nextDist = ((Node*)rst)[i + 1].dist + cost;
				Point* there = ((Node*)rst)[i + 1].self;
				// 发现更短路径时，更新dist[]并添加到nodeSet

				/* 处理dist列表
				** 如果dist列表里没有there点，则将其添加进去
				** 如果dist列表里有there点，且其dist比nextDist大，则更新dist
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
				/* 处理nodeSet边界点集合
				** 当前点是末点，则不再将其添入边界点集合中
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