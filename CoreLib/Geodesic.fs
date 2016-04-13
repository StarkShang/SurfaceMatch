module Geodesic
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
        member self.AddVertex(pt:(Vertex*(int*int))) =
            let value = pt|>fst
            let row = pt|>snd|>fst
            let col = pt|>snd|>snd
            graph.[row,col]<-value
    end

let GetShortestPath (graph:Graph) (startPt:(int*int)) (endPt:(int*int)) =
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
            for pt in graph.GetAdjacent(here) do
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
    (dist,(record|>List.toArray))