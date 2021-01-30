using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {

    public enum CardArcana{
        MAJOR,
        MINOR
    }

    public enum CardFamily{
        NONE,
        WISPS,
        TBD1,
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

    public Card(string _name, CardArcana _arcana, Sprite _sprite){
        this.cardName = _name; 
        this.arcana = _arcana;  
        this.sprite = _sprite;
    }

}

public class MajorArcanaCard : Card {
    
    public CardMajorType type;

    public MajorArcanaCard(string _name, CardArcana _arcana, CardMajorType _type, Sprite _sprite) : base(_name, _arcana, _sprite)
    {
        this.cardName = _name;
        this.arcana = _arcana;
        this.type = _type;
        this.sprite = _sprite;
    }
}

public class MinorArcanaCard : Card {

    public CardFamily family;
    public int value;

    public MinorArcanaCard(string _name, CardArcana _arcana, CardFamily _family, int _value, Sprite _sprite) : base(_name, _arcana, _sprite)
    {
        this.cardName = _name;
        this.arcana = _arcana;
        this.family = _family;
        this.value = _value;
        this.sprite = _sprite;
    }
}

