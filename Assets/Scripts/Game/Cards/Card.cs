using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card", order = 0)]
public class Card : ScriptableObject {

    public enum CardArcana{
        MAJOR,
        MINOR
    }

    public enum CardFamily{
        NONE,
        WISPS,
        DICE,
        TBD2,
        TBD3
    }

    public enum CardMajorType{
        NONE,
        WHEELOFFORTUNE,
        TOWER
    }

    public string cardName;
    public CardArcana arcana;
    public Sprite sprite;
    public string description;

    public Card(string _name, CardArcana _arcana, Sprite _sprite, string _description){
        this.cardName = _name; 
        this.arcana = _arcana;  
        this.sprite = _sprite;
        this.description = _description;
    }

}

[CreateAssetMenu(fileName = "MajorArcana", menuName = "Cards/MajorArcana", order = 0)]
public class MajorArcanaCard : Card {
    
    public CardMajorType type;

    public MajorArcanaCard(string _name, CardArcana _arcana, CardMajorType _type, Sprite _sprite, string _description) : base(_name, _arcana, _sprite, _description)
    {
        this.cardName = _name;
        this.arcana = _arcana;
        this.type = _type;
        this.sprite = _sprite;
        this.description = _description;
    }
}

[CreateAssetMenu(fileName = "MinorArcana", menuName = "Cards/MinorArcana", order = 0)]
public class MinorArcanaCard : Card {

    public CardFamily family;
    public int value;

    public MinorArcanaCard(string _name, CardArcana _arcana, CardFamily _family, int _value, Sprite _sprite, string _description) : base(_name, _arcana, _sprite, _description)
    {
        this.cardName = _name;
        this.arcana = _arcana;
        this.family = _family;
        this.value = _value;
        this.sprite = _sprite;
        this.description = _description;
    }
}

