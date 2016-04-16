#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "definition.h"

float calEuclDis(Point* pt1,Point* pt2) {
  return sqrt(
    pow(pt1->x-pt2->x,2)+
    pow(pt1->y-pt2->y,2)+
    pow(pt1->z-pt2->z,2)
  );
}

//__declspec(dllexport)
void calRelativeDis(Point** pts, int row, int col) {
  for (int i = 0; i < row; i++) {
    for (int j = 0; j < col; j++) {
      if (i > 0)
        pts[i][j].top = calEuclDis(&pts[i][j], &pts[i-1][j]);
      if (i < row-1)
        pts[i][j].bottom = calEuclDis(&pts[i][j], &pts[i+1][j]);
      if (j > 0)
        pts[i][j].left = calEuclDis(&pts[i][j], &pts[i][j-1]);
      if (j < col-1)
        pts[i][j].right = calEuclDis(&pts[i][j], &pts[i][j+1]);
    }
  }
}
