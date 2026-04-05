using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SimpleAITagPlayer : MonoBehaviour
{
    public int AIUpdateCooldown = 10;
    private int framecount = 0;
    private TagOpponent[] opponents;
    private TagOpponent closest;

    void Update() {
        if (framecount++ < AIUpdateCooldown) return;
        else framecount = 0;
        if ((opponents?.Length ?? 0) < 25) {
            opponents = GameObject.FindObjectsByType<TagOpponent>(FindObjectsSortMode.None);
        }

        if (this.GetComponent<TagPlayer>().isIt) {
            this.GetComponent<Flee>().enabled = false;
            this.GetComponent<Persue>().enabled = true;
            foreach (TagOpponent opponent in opponents) {                    
                if (
                    closest == null ||
                    Vector3.Distance(this.transform.position, opponent.transform.position) <
                    Vector3.Distance(this.transform.position, closest.transform.position)
                ) {
                    closest = opponent;
                    this.GetComponent<Persue>().target = closest.gameObject;
                }
            }
        }
        else {
            this.GetComponent<Persue>().enabled = false;
            this.GetComponent<Flee>().enabled = true;
            foreach (TagOpponent opponent in opponents) {
                if (opponent.isIt) {
                    this.GetComponent<Flee>().setFearedPosition(opponent.transform.position);
                    break;
                }
            }
        }
    }
}
