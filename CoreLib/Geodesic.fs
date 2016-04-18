module Geodesic

open Types

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