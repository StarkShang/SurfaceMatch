#include <stdlib.h>
#include <stdio.h>
#include "node.h"

_declspec(dllexport)
void createMesh(int row, int col) {
	Row = row;
	Col = col;
	mesh.nodes = (Point**)malloc(row * sizeof(Point*));
	for (int i = 0; i < row; i++) {
		mesh.nodes[i] = (Point*)malloc(col * sizeof(Point*));
	}
}

__declspec(dllexport)
void destroyMesh() {
	for (int i = 0; i < Row; i++) {
		free(mesh.nodes[i]);
	}
	free(mesh.nodes);
}

__declspec(dllexport)
void addNode(float x, float y, float z, int* row, int* col) {
	if (mesh.nodes != NULL) {
		if (row >= 0 && row <= Row) {
			if (col >= 0 && col <= Col) {
				mesh.nodes[*row][*col].x = x;
				mesh.nodes[*row][*col].y = y;
				mesh.nodes[*row][*col].z = z;
			}
		}
	}
}

__declspec(dllexport)
void printMesh() {
	for (int i = 0; i < Row; i++)
	{
		for (int j = 0; j < Col; j++)
		{
			printf("(%f,%f,%f)", mesh.nodes[i][j].x, mesh.nodes[i][j].y, mesh.nodes[i][j].z);
		}
		printf("\n");
	}
}

__declspec(dllexport)
void* getAdjacent(Node* node) {
	int row = node->row;
	int col = node->col;
	Node head;
	Node* tail = &head;
	if (row > 0) {
		Node node;
		node.self = &mesh.nodes[row - 1][col];
		node.row = row - 1;
		node.col = col;
		node.dist = mesh.nodes[row][col].top;
		// 将节点添加到链表尾部
		tail->next = &node;
		node.previous = tail;
		tail = &node;
	}
	if (row < Row - 1) {
		Node node;
		node.self = &mesh.nodes[row + 1][col];
		node.row = row + 1;
		node.col = col;
		node.dist = mesh.nodes[row][col].bottom;
		// 将节点添加到链表尾部
		tail->next = &node;
		node.previous = tail;
		tail = &node;
	}
	if (col > 0) {
		Node node;
		node.self = &mesh.nodes[row][col - 1];
		node.row = row;
		node.col = col - 1;
		node.dist = mesh.nodes[row][col].left;
		// 将节点添加到链表尾部
		tail->next = &node;
		node.previous = tail;
		tail = &node;
	}
	if (col < Col - 1) {
		Node node;
		node.self = &mesh.nodes[row][col + 1];;
		node.row = row;
		node.col = col + 1;
		node.dist = mesh.nodes[row][col].right;
		// 将节点添加到链表尾部
		tail->next = &node;
		node.previous = tail;
		tail = &node;
	}
	return &head;
}

/*
** Node链表的操作
*/
// 查询链表是否包含结点
int contains(Node* node, Point* point) {
	if (node->self == point)
		return 1;
	else if (node->next == NULL)
		return 0;
	else contains(node->next, point);
}
// 寻找指定结点
Node* find(Node* node, Point* point) {
	if (node->self == point)
		return node;
	else if (node->next == NULL)
		return NULL;
	else contains(node->next, point);
}
// 排序操作
void sortNodeChain(Node** head) {
	Node* node = (*head)->next;
	while (node != NULL)
	{
		// 将当前节点从队列中取出
		Node* target = node->previous;
		Node* next = node->next;
		target->next = next;
		if (next != NULL)
		{
			next->previous = target;
		}
		// 遍历当前结点前面的有序队列
		while (target != NULL)
		{
			if (target->dist > node->dist) {
				if (target->previous == NULL) {
					node->previous = NULL;
					node->next = target;
					target->previous = node;
					(*head) = node;
					break;
				}
				else {
					target = target->previous;
				}
			} 
			else {
				// 将node插入有序队列
				node->previous = target;
				node->next = target->next;
				target->next = node;
				node->next->previous = node;
				break;
			}
			
		}
		// 将node变成下一个节点
		node = next;
	}
}