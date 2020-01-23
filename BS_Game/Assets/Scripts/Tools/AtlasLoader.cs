using System.Collections.Generic;
using UnityEngine;

namespace BSGame.Tools
{
    public class AtlasLoader 
    {
        public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

        //Creates new Instance and Loads the provided sprites
        public AtlasLoader(string spriteBaseName)
        {
            LoadSprite(spriteBaseName);
        }

        //Loads the provided sprites
        private void LoadSprite(string spriteBaseName)
        {
            var allSprites = Resources.LoadAll<Sprite>(spriteBaseName);
            if (allSprites == null || allSprites.Length <= 0)
            {
                Debug.LogError("The Provided Base-Atlas Sprite `" + spriteBaseName + "` does not exist!");
                return;
            }

            for (var i = 0; i < allSprites.Length; i++)
            {
                spriteDic.Add(allSprites[i].name, allSprites[i]);
            }
        }

        //Get the provided atlas from the loaded sprites
        public Sprite GetSprite(string spriteName)
        {
            Sprite tempSprite;

            if (!spriteDic.TryGetValue(spriteName, out tempSprite))
            {
                Debug.LogError("The Provided atlas `" + spriteName + "` does not exist!");
                return null;
            }
            return tempSprite;
        }

    }
}
