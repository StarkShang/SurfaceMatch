module Types

type Vertex (left:float,top:float,right:float,bottom:float) as self =
    class
        let left = left
        let top = top
        let right = right
        let bottom = bottom
        member self.Left 
            with get() = left
        member self.Top 
            with get() = top
        member self.Right 
            with get() = right
        member self.Bottom 
            with get() = bottom
    end

type Graph (rowNumber:int,colNumber:int) as self =
    class
        let rowNumber = rowNumber
        let colNumber = colNumber
        let graph = Array2D.create rowNumber colNumber (new Vertex(0.0,0.0,0.0,0.0))
        // 获得周围点
        member self.GetAdjacent(pt:(int*int)) =
            let mutable rst = []
            let row = pt|>fst
            let col = pt|>snd
            if row > 0 then
                rst<-(graph.[row,col].Top,(row-1,col))::rst
            if row < rowNumber-1 then
                rst<-(graph.[row,col].Bottom,(row+1,col))::rst
            if col > 0 then 
                rst<-(graph.[row,col].Left,(row,col-1))::rst
            if col < colNumber-1 then 
                rst<-(graph.[row,col].Right,(row,col+1))::rst
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
            let rst = dist|>Array.find(fun item->((item|>snd|>fst)=(endPt|>fst))&&((item|>snd|>snd)=(endPt|>snd)))|>fst
            rst
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
            for i = 0 to row do
                for j = 0 to col do
                    let mutable left = 0.0
                    let mutable top = 0.0
                    let mutable right = 0.0
                    let mutable bottom = 0.0
                    if i > 0 then 
                        top <- self.calEuclidDist((mesh.[i,j]), (mesh.[i-1,j]))
                    if i < row - 1 then
                        bottom <- self.calEuclidDist((mesh.[i,j]), (mesh.[i+1,j]))
                    if j > 0 then
                        left <- self.calEuclidDist((mesh.[i,j]), (mesh.[i,j-1]))
                    if j < col - 1 then
                        right <- self.calEuclidDist((mesh.[i,j]), (mesh.[i,j+1]))
                    let vertex = new Vertex(left, top, right, bottom)
                    graph.AddVertex(vertex, i, j)
            graph
    end