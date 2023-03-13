using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public const int cols = 3;
    public const int rows = 3;

    public const float xident = 4f;
    public const float yident = 2f;

    [SerializeField] private MainImageScript startingCard;
    [SerializeField] private Sprite[] cards;

    private int[] Shuffler(int[] locations)
    {
        int[] deck = locations.Clone() as int[];
        for(int i=0; i < deck.Length; i++)
        {
            int newDeck = deck[i];
            int j = Random.Range(i, deck.Length);
            deck[i] = deck[j];
            deck[j] = newDeck;
        }
        return deck;
    }

    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3, 4};
        locations = Shuffler(locations);

        Vector3 startPos = startingCard.transform.position;

        for(int i = 0; i < cols; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                MainImageScript card;
                if(i == 0 && j == 0)
                {
                    card = startingCard;
                }
                else
                {
                    card = Instantiate(startingCard) as MainImageScript;
                }

                int index = j * cols + i;
                int id = locations[index];
                card.ChangeSprite(id, cards[id]);

                float posX = (xident * i) + startPos.x;
                float posY = -(yident * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private MainImageScript firstFlip;
    private MainImageScript secondFlip;

    private int score = 0;
    private int attempt = 0;

    [SerializeField] private TMPro.TextMeshPro Scoreboard;
    [SerializeField] private TMPro.TextMeshPro Attempts;
    [SerializeField] private TMPro.TextMeshPro WinScreen;

    public bool canFlip
    {
        get { return secondFlip == null; }
    }

    public void cardFlipped(MainImageScript startingCard)
    {
        if(firstFlip == null)
        {
            firstFlip = startingCard;
        }
        else
        {
            secondFlip = startingCard;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstFlip.spriteId == secondFlip.spriteId)
        {
            score++;
            Scoreboard.text = "Score: " + score;
            yield return new WaitForSeconds(0.5f);
            firstFlip.Remove();
            secondFlip.Remove();
            if (score == 4)
            {
                WinScreen.gameObject.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstFlip.Close();
            secondFlip.Close();
        }

        attempt++;
        Attempts.text = "Attempt: " + attempt;

        firstFlip = null;
        secondFlip = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
