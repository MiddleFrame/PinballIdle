using System;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
   [SerializeField]
   private Image _stroke;

   [SerializeField]
   private ColorElement[] _elements;

   [SerializeField]
   private Color _grayColor;
   [SerializeField]
   private Color _strokeColor;

   private bool _isColor;

   public void Color()
   {
      if (_isColor) return;
      _isColor = true;
      _stroke.color =_strokeColor;
      foreach (var _element in _elements)
      {
         foreach (var _elementElement in _element.elements)
         {
            _elementElement.color = _element.colors;
         }
      }  
   }

   public void RemoveColor()
   {
      if (!_isColor) return;
      _isColor = false;
      _stroke.color = _grayColor;
      foreach (var _element in _elements)
      {
         foreach (var _elementElement in _element.elements)
         {
            _elementElement.color = _grayColor;
         }
      }
   }

}

[Serializable]
public class ColorElement
{
   public Graphic[] elements;

      public Color colors;
}
