using System.Collections.Generic;
using UnityEngine;

public class CharacterOutfit : MonoBehaviour
{
    // 0: Hood, 1: Face, 2: Shoulder Left, 3: Shoulder Right, 4: Elbow Left, 5: Elbow Right
    // 6: Wrist Left, 7: Wrist Right, 8: Torso, 9: Pelvis, 10: Leg Left, 11: Leg Right
    // 12: Boot Left, Boot Right
    public List<SpriteRenderer> outfitParts;

    public void Equip(Sprite[] sprite, int[] bodyPartIndex)
    {
        for (int i = 0; i < sprite.Length; i++)
        {            
            outfitParts[bodyPartIndex[i]].sprite = sprite[i];
        }
    }
}
