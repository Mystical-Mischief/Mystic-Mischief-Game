using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketTrackerTemp : MonoBehaviour
{
    public List<GameObject> allTickets;
    public int ticketCollectNum;
    public GameObject birdActivate;
    public int currTicketNum;

    void Update()
    {
        if(currTicketNum == ticketCollectNum)
        {
            birdActivate.SetActive(true);
        }
    }
}
