using System;
using Harmony;

namespace Script.Character.Player
{
    public class PlayerInventory
    {
        private int[,] runes;

        public PlayerInventory()
        {
            int runeElementsAmounts = Enum.GetNames(typeof(Elements)).Length - 1;
            int runeSizeAmounts = Enum.GetNames(typeof(RuneSize)).Length;
            
            runes = new int[runeElementsAmounts, runeSizeAmounts];

            PopulateRunesArray(runeElementsAmounts, runeSizeAmounts);
        }

        private void PopulateRunesArray(int runeElementsAmounts, int runeSizeAmounts)
        {
            for (int i = 0; i < runeElementsAmounts; i++)
            {
                for (int j = 0; j < runeSizeAmounts; j++)
                {
                    runes[i, j] = 0;
                }
            }
        }

        public void AddRune(int amount, Elements element, RuneSize size)
        {
            runes[(int)element, (int)size] += amount;
        }

        //Retourne faux si il n'y a pas assez de rune
        public bool RemoveRune(int amount, Elements element, RuneSize size)
        {
            if (runes[(int) element, (int) size] >= amount)
            {
                runes[(int) element, (int) size] -= amount;
                return true;
            }

            return false;
        }

        public int GetRuneQuantity(Elements element, RuneSize size)
        {
            return runes[(int) element, (int) size];
        }
    }
}