using UnityEngine;

public class RuneZone : MonoBehaviour
{
        [SerializeField] private Rune runeToSpawn;
        [SerializeField] private int amountToSpawn = 25;

        private Rune[] spawnedRunes;
        private float minX;
        private float maxX;
        private float minY;
        private float maxY;

        private void Start()
        {
                spawnedRunes = new Rune[amountToSpawn];
                float halfX = transform.localScale.x / 2;
                float halfY = transform.localScale.y / 2;
                minX = transform.position.x - halfX;
                maxX = transform.position.x + halfX;
                minY = transform.position.y - halfY;
                maxY = transform.position.y + halfY;
                ResetRunes();
        }

        public void ResetRunes()
        {
                foreach (Rune rune in spawnedRunes)
                {
                        if (rune != null)
                                Destroy(rune.gameObject);
                }

                for (int i = 0; i < amountToSpawn; i++)
                {
                        spawnedRunes[i] = Instantiate(runeToSpawn, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
                }
        }
}