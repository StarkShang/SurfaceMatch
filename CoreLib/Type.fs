module Types

type Vertex (left:float,top:float,right:float,bottom:float,leftTop:float,rightTop:float,leftBottom:float,rightBottom:float) as self =
    class
        let left = left
        let top = top
        let right = right
        let bottom = bottom
        let leftTop = leftTop
        let rightTop = rightTop
        let leftBottom = leftBottom
        let rightBottom = rightBottom

        member self.Left 
            with get() = left
        member self.Top 
            with get() = top
        member self.Right 
            with get() = right
        member self.Bottom 
            with get() = bottom
        member self.LeftTop
            with get() = leftTop
        member self.RightTop
            with get() = rightTop
        member self.LeftBottom
            with get() = leftBottom
        member self.RightBottom
            with get() = rightBottom
    end

// 图结构
type Graph (rowNumber:int,colNumber:int) as self =
    class
        let rowNumber = rowNumber
        let colNumber = colNumber
        let graph = Array2D.create rowNumber colNumber (new Vertex(0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0))
        // 获得周围点
        member self.GetAdjacent(pt:(int*int)) =
            let mutable rst = []
            let row = pt|>fst
            let col = pt|>snd

            // 若不是上边界上的点，则取出上面的点
            if (row>0) then
                rst<-(graph.[row,col].Top,(row-1,col))::rst
                // 若此时不是做边界上的点，则取出左上点
                if (col>0) then rst<-(graph.[row,col].LeftTop,(row-1,col-1))::rst
                // 若此时不是做边界上的点，则取出左上点
                if (col<colNumber-1) then rst<-(graph.[row,col].RightTop,(row-1,col+1))::rst
            // 若不是下边界上的点，则取出下面的点
            if (row<rowNumber-1) then
                rst<-(graph.[row,col].Top,(row+1,col))::rst
                // 若此时不是做边界上的点，则取出左下点
                if (col>0) then rst<-(graph.[row,col].LeftBottom,(row+1,col-1))::rst
                // 若此时不是做边界上的点，则取出左下点
                if (col<colNumber-1) then rst<-(graph.[row,col].RightBottom,(row+1,col+1))::rst
            // 若不是左边界上的点，则取出左面的点
            if (col>0) then rst<-(graph.[row,col].Left,(row,col-1))::rst
            // 若不是右边界上的点，则取出右面的点
            if (col<colNumber-1) then rst<-(graph.[row,col].Right,(row,col+1))::rst
            rst
        // 往图中添加元素
        member public self.AddVertex((pt:Vertex),(row:int),(col:int)) =
            graph.[row,col]<-pt
        // 获得最短路径
        member public self.getShortestPath (startPt:(int*int), endPt:(int*int)) =
            let mutable vertexSet = [(0.0,startPt)]
            let mutable dist = [|(0.0,startPt)|]
            let mutable record = []

            while ((endPt,vertexSet|>List.unzip|>snd)||>List.contains) 
                ||((endPt,dist|>Array.unzip|>snd)||>Array.contains|>not)
                ||((dist|>Array.find(fun item->((item|>snd|>fst)=(endPt|>fst))&&((item|>snd|>snd)=(endPt|>snd)))|>fst)>(vertexSet.Head|>fst))
                do
                let cost = vertexSet.Head|>fst
                let here = vertexSet.Head|>snd
                record<-here::record
                vertexSet<-vertexSet.Tail
                let hereDist = dist|>Array.find(fun item->((item|>snd|>fst)=(here|>fst))&&((item|>snd|>snd)=(here|>snd)))|>fst
                if hereDist >= cost then
                    for pt in self.GetAdjacent(here) do
                        let nextDist = (pt|>fst) + cost
                        let there = pt|>snd
                        // 发现更短路径时，更新dist[]并添加到vertexSet
                        if (there,dist|>Array.unzip|>snd)||>Array.contains then
                            let thereDistIndex = dist|>Array.findIndex(fun item->((item|>snd|>fst=(there|>fst))&&(item|>snd|>snd=(there|>snd))))
                            if (dist.[thereDistIndex]|>fst) > nextDist then
                                dist.[thereDistIndex]<-(nextDist,there)
                                if (there|>fst <> (endPt|>fst)) || (there|>snd <> (endPt|>snd)) then
                                    vertexSet<-(nextDist,there)::vertexSet
                        else
                            dist<-Array.append dist [|(nextDist,there)|]
                            if (there|>fst <> (endPt|>fst)) || (there|>snd <> (endPt|>snd)) then
                                vertexSet<-(nextDist,there)::vertexSet
                vertexSet<-vertexSet|>List.sortBy(fun elem->(elem|>fst))
            record<-record|>List.rev
            let rcd = record|>List.toArray
            let rst = dist|>Array.find(fun item->((item|>snd|>fst)=(endPt|>fst))&&((item|>snd|>snd)=(endPt|>snd)))|>fst
            rst
        //给定一个点获取它到周围三个定点的测地线
        member public self.getGeodesics(row:int, col:int, step:int) = 
            match row,col with
            |(_,_) when(row=col&&row+col<rowNumber) -> 
                let dist1 = self.getShortestPath((row,col),(row,col-step))
                let dist2 = self.getShortestPath((row,col),(row-step,col-step))
                let dist3 = self.getShortestPath((row,col),(row-step,col))
                (dist1, dist2, dist3)
            |(_,_) when(row=col&&row+col>rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row,col+step))
                let dist2 = self.getShortestPath((row,col),(row+step,col+step))
                let dist3 = self.getShortestPath((row,col),(row+step,col))
                (dist1, dist2, dist3)
            |(_,_) when(row<col&&row+col=rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row,col+step))
                let dist2 = self.getShortestPath((row,col),(row-step,col+step))
                let dist3 = self.getShortestPath((row,col),(row-step,col))
                (dist1, dist2, dist3)
            |(_,_) when(row>col&&row+col=rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row,col-step))
                let dist2 = self.getShortestPath((row,col),(row+step,col-step))
                let dist3 = self.getShortestPath((row,col),(row+step,col))
                (dist1, dist2, dist3)
            |(_,_) when(row<col&&row+col<rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row-step,col-step))
                let dist2 = self.getShortestPath((row,col),(row-step,col))
                let dist3 = self.getShortestPath((row,col),(row-step,col+step))
                (dist1, dist2, dist3)
            |(_,_) when(row<col&&row+col>rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row-step,col+step))
                let dist2 = self.getShortestPath((row,col),(row,col+step))
                let dist3 = self.getShortestPath((row,col),(row+step,col+step))
                (dist1, dist2, dist3)
            |(_,_) when(row>col&&row+col<rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row-step,col-step))
                let dist2 = self.getShortestPath((row,col),(row,col-step))
                let dist3 = self.getShortestPath((row,col),(row+step,col-step))
                (dist1, dist2, dist3)
            |(_,_) when(row>col&&row+col>rowNumber) ->
                let dist1 = self.getShortestPath((row,col),(row+step,col-step))
                let dist2 = self.getShortestPath((row,col),(row+step,col))
                let dist3 = self.getShortestPath((row,col),(row+step,col+step))
                (dist1, dist2, dist3)
            |_ ->
                let dist1 = self.getShortestPath((row,col),(row-step,col))
                let dist2 = self.getShortestPath((row,col),(row,col+step))
                let dist3 = self.getShortestPath((row,col),(row,col-step))
                (dist1, dist2, dist3)
    end

// 记录空间中的点
type Node (x:float, y:float, z:float) =
    class
        let x = x;
        let y = y;
        let z = z;
        member self.X
            with get() = x
        member self.Y
            with get() = y
        member self.Z
            with get() = z
    end

type Mesh (row, col) = 
    class
        let row = row
        let col = col
        let mesh = Array2D.create row col (new Node(0.0, 0.0, 0.0))
        
        member public self.setNode(node:Node, row:int, col:int) =
            mesh.[row,col]<-node
        member self.calEuclidDist(pt1:Node, pt2:Node) =
            sqrt(
                (pown (pt1.X - pt2.X) 2)
                + (pown (pt1.Y - pt2.Y) 2)
                + (pown (pt1.Z - pt2.Z) 2))
        member self.generateGraph() =
            let graph = new Graph(row, col)
            for i = 0 to row-1 do
                for j = 0 to col-1 do
                    let mutable left = 0.0
                    let mutable top = 0.0
                    let mutable right = 0.0
                    let mutable bottom = 0.0
                    let mutable leftTop = 0.0
                    let mutable rightTop = 0.0
                    let mutable leftBottom = 0.0
                    let mutable rightBottom = 0.0
                    // 若不是上边界上的点，则存在上面的距离
                    if (i>0) then
                        top <- self.calEuclidDist(mesh.[i,j],mesh.[i-1,j])
                        // 若此时不是做边界上的点，则存在左上距离
                        if (j>0) then leftTop <- self.calEuclidDist(mesh.[i,j],mesh.[i-1,j-1])
                        // 若此时不是做边界上的点，则存在左上距离
                        if (j<col-1) then rightTop <- self.calEuclidDist(mesh.[i,j],mesh.[i-1,j+1])
                    // 若不是下边界上的点，则存在下面的距离
                    if (i<row-1) then
                        bottom <- self.calEuclidDist(mesh.[i,j],mesh.[i+1,j])
                        // 若此时不是做边界上的点，则存在左下距离
                        if (j>0) then leftBottom <- self.calEuclidDist(mesh.[i,j],mesh.[i+1,j-1])
                        // 若此时不是做边界上的点，则存在左下距离
                        if (j<col-1) then rightBottom <- self.calEuclidDist(mesh.[i,j],mesh.[i+1,j+1])
                    // 若不是左边界上的点，则存在左面的距离
                    if (j>0) then left <- self.calEuclidDist(mesh.[i,j],mesh.[i,j-1])
                    // 若不是右边界上的点，则存在右面的距离
                    if (j<col-1) then right <- self.calEuclidDist(mesh.[i,j],mesh.[i,j+1])
                    let vertex = new Vertex(left, top, right, bottom, leftTop, rightTop, leftBottom, rightBottom)
                    graph.AddVertex(vertex, i, j)
            graph
    end