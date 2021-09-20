using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : MonoBehaviour
{
    [SerializeField]
    public GameObject ball;
    
    int mazeWidth = 22;
    int mazeDepth = 19;

    int[,] maze = new int[19, 22] {
        { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
        { 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1 },
        { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 }
    };

    public struct Location
    {
        public readonly int x, y;
        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static bool operator ==(Location l1, Location l2) 
        {
            return l1.x == l2.x && l1.y == l2.y;
        }
        public static bool operator !=(Location l1, Location l2) 
        {
            return l1.x != l2.x || l1.y != l2.y;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var objectToCompareWith = (Location) obj;

            return this == objectToCompareWith;

        }
        public override int GetHashCode()
        {
            return $"{x} {y}".GetHashCode();
        }
        public override string ToString() => $"({x}, {y} [{Random.Range(0.0f, 9999999.0f)}])";
    }

    Queue<Location> frontier = new Queue<Location>();
    Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
    Location currentPos;

    //destino
    //montar caminho de trás pra frente

    public void Start(){

        Location posStart = new Location(0, 1);
        Location posTarget = new Location(18, 20);
        frontier.Enqueue(posStart);

        while(frontier.Count != 0){

            currentPos = frontier.Dequeue();
        // Debug.Log(currentPos);
            Queue<Location> neighbours = new Queue<Location>();

            //check neighbors
            //x line, y column
            //left
            if(currentPos.y > 0){
            neighbours.Enqueue(new Location(currentPos.x, currentPos.y - 1));
            }
            //right
            if(currentPos.y < mazeWidth - 1){
            neighbours.Enqueue(new Location(currentPos.x, currentPos.y + 1));
            }
            //up
            if(currentPos.x > 0){
            neighbours.Enqueue(new Location(currentPos.x - 1, currentPos.y));
            }
            //down
            if(currentPos.x < mazeDepth - 1){
            neighbours.Enqueue(new Location(currentPos.x + 1, currentPos.y));
            }

            foreach (Location element in neighbours){  
                if (maze[element.x, element.y] != 1){
                    if (!cameFrom.ContainsKey(element)){
                        //Debug.Log("added " + element);
                        frontier.Enqueue(element);
                        cameFrom[element] = currentPos;
                    }
                } else {
                    //Debug.Log("wall " + element);
                }
            }
        }

        List<Location> path = new List<Location>();
        currentPos = posTarget;

        while(currentPos != posStart) {
            path.Add(currentPos);
            currentPos = cameFrom[currentPos];
        }
        path.Add(posStart);
        path.Reverse();

        foreach (Location pos in path) {
            Debug.Log(pos);
            Instantiate(ball, new Vector3(pos.y * 2, 5, -pos.x * 2), Quaternion.identity);
        }

    }
}
