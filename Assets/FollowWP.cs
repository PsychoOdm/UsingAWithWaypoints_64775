using UnityEngine;

public class FollowWP : MonoBehaviour {

    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start() {

        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];

        Time.timeScale = 5.0f;
    }

    public void GoToHeli() {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    public void GoToRuin() {
        g.AStar(currentNode, wps[8]);
        currentWP = 0;
    }

    public void GoToOil() {
        g.AStar(currentNode, wps[5]);
        currentWP = 0;
    }

    public void GoToRocks() {
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

    
    public void GoToFactor() {
        g.AStar(currentNode, wps[2]); // Podes alterar o 2 se o waypoint do Factor for outro
        currentWP = 0;
    }

    bool debbuged = false;

    void LateUpdate() {

        if (g.pathList.Count == 0 || currentWP == g.pathList.Count) return;

        if (!debbuged) {

            Debug.Log(g.pathList.Count);
            debbuged = true;
        }

        if (Vector3.Distance(g.pathList[currentWP].getId().transform.position,
            this.transform.position) < accuracy) {

            currentNode = g.pathList[currentWP].getId();
            currentWP++;
        }

        if(currentWP < g.pathList.Count) {

            goal = g.pathList[currentWP].getId().transform;

            Vector3 lookAtGoal = new Vector3(
                goal.position.x,
                this.transform.position.y,
                goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(
                this.transform.rotation, 
                Quaternion.LookRotation(direction), 
                Time.deltaTime * rotSpeed);

            this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
    }
}