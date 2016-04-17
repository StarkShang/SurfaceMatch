#include <stdio.h>
#include "node.h"

void test1();
void test2();

int main ()
{
	test2();
	return 0;
}

void test1() {
	printf("begin\n");

	Node node1;
	node1.dist = 5;
	Node node2;
	node2.dist = 8;
	Node node3;
	node3.dist = 4;
	Node node4;
	node4.dist = 2;
	Node node5;
	node5.dist = 3;
	Node node6;
	node6.dist = 7;
	Node node7;
	node7.dist = 1;
	Node node8;
	node8.dist = 9;
	Node node9;
	node9.dist = 6;

	node1.previous = NULL;
	node1.next = &node2;
	node2.previous = &node1;
	node2.next = &node3;
	node3.previous = &node2;
	node3.next = &node4;
	node4.previous = &node3;
	node4.next = &node5;
	node5.previous = &node4;
	node5.next = &node6;
	node6.previous = &node5;
	node6.next = &node7;
	node7.previous = &node6;
	node7.next = &node8;
	node8.previous = &node7;
	node8.next = &node9;
	node9.previous = &node8;
	node9.next = NULL;

	Node* head = &node1;
	//sortNodeChain(&head);


	Node* node = head;
	while (node->next != NULL) {
		printf("%f\n", node->dist);
		node = node->next;
	}
}

void test2() {
	createMesh(3, 3);
	int a = 0;
	int b = 1;
	int c = 2;
	addNode(0.0, 0.0, 0.0, &a, &a);
	addNode(0.0, 1.0, 0.0, &a, &b);
	addNode(0.0, 2.0, 0.0, &a, &c);
	addNode(1.0, 0.0, 0.0, &b, &a);
	addNode(1.0, 1.0, 0.0, &b, &b);
	addNode(1.0, 2.0, 0.0, &b, &c);
	addNode(2.0, 0.0, 0.0, &c, &a);
	addNode(2.0, 1.0, 0.0, &c, &b);
	addNode(2.0, 2.0, 0.0, &c, &c);
	printMesh();
	destroyMesh();
}