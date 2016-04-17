#ifndef _DEFINITION_H_
#define _DEFINITION_H_

#define Point struct point
__declspec(dllexport)
Point{
  float x;
  float y;
  float z;
  float left;
  float top;
  float right;
  float bottom;
};

#define Mesh struct mesh
__declspec(dllexport)
Mesh{
	Point** nodes;
};
Mesh mesh;


#define Node struct node
__declspec(dllexport)
Node{
	Point* self;
	float dist;
	int row;
	int col;

	Node* next;
	Node* previous;
};

Mesh mesh;
int Row;
int Col;

int contains(Node* node, Point* point);
Node* find(Node* node, Point* point);
void sortNodeChain(Node** head);
_declspec(dllexport) void createMesh(int row, int col);

#endif
