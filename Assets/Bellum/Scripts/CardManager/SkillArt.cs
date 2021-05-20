using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillArt : MonoBehaviour
{
    [Serializable]
    public struct SkillImage
    {
        public SpecialAttackDict.SpecialAttackType skillType ;
        public Sprite image;
    }
    public SkillImage[] skillImages;
    public Dictionary<string, SkillImage> SkillArtDictionary = new Dictionary<string, SkillImage>();

    public void Start()
    {
        initDictionary();
    }
    public void initDictionary()
    {
        SkillArtDictionary.Clear();
        foreach (SkillImage image in skillImages)
        {
            SkillArtDictionary.Add(image.skillType.ToString(), image);
        }
    }
}
