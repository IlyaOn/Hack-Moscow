using UnityEngine;
public class MyButton
{
    public Vector2 Position
    {
        get;
        private set;
    }
    public Vector2 Size
    {
        get;
        private set;
    }
    public Rect Rectangle
    {
        get;
        private set;
    }
    private Texture2D _texNormal;//без мыши
    private Texture2D _texHover;//при наведении
    private Texture2D _texActive;//при нажатии
    private ButtonState _state;//состояние кнопки
    /// <summary>
    /// Возвращает текущую текстуру,которая зависит от состояния кнопки
    /// </summary>
    private Texture2D CurrentTexture
    {
        get
        {
            switch (_state)
            {
                case ButtonState.Normal:
                    return _texNormal;
                case ButtonState.Hover:
                    return _texHover;
                case ButtonState.Active:
                    return _texActive;
                default: return _texActive;
            }
        }
    }
    //конструктор
    public MyButton(Vector2 pos, Vector2 size, Texture2D tNormal, Texture2D tHover, Texture2D tActive)
    {
        SetPosition(pos);
        SetSize(size);
        _texNormal = tNormal;
        _texHover = tHover;
        _texActive = tActive;
        _state = ButtonState.Normal;
    }
    /// <summary>
    /// Метод для установки размера кнопки
    /// </summary>
    /// <param name="size"></param>
    public void SetSize(Vector2 size)
    {
        Size = size;
        Rectangle = new Rect(Position.x, Position.y, Size.x, Size.y);
    }
    public void SetSize(int width, int height)
    {
        Size = new Vector2(width, height);
        Rectangle = new Rect(Position.x, Position.y, Size.x, Size.y);
    }
    /// <summary>
    /// Метод для установки позиции кнопки
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector2 pos)
    {
        Position = pos;
        Rectangle = new Rect(Position.x, Position.y, Size.x, Size.y);
    }
    public void SetPosition(int x, int y)
    {
        Position = new Vector2(x, y);
        Rectangle = new Rect(Position.x, Position.y, Size.x, Size.y);
    }
    /// <summary>
    /// Обработка логики кнопки
    /// </summary>
    /// <param name="mousePos">Позиция курсора</param>
    /// <param name="pressed">Зажата ли левая кнопка мыши?</param>
    /// <returns>Возвращает True, если произошел клик</returns>
    public bool Update(Vector2 mousePos, bool pressed)
    {
        //т.к. координаты мыши начинаются с левого нижнего угла,
        // а "наши" координаты с левого правого
        mousePos = new Vector2(mousePos.x, Screen.height - mousePos.y);
        //входит ли точка курсора в область кнопки
        if (Rectangle.Contains(mousePos))
        {
            //полчаем прозрачность пикселя в точке, над которой находится курсор
            float a = CurrentTexture.GetPixel((int)mousePos.x, (int)(Position.y + Size.y - mousePos.y)).a;
            //если не прозрачный пиксель
            if (a != 0)
            {
                ButtonState oldState = _state;
                _state = pressed ? ButtonState.Active : ButtonState.Hover;
                if (oldState == ButtonState.Hover && _state == ButtonState.Active)
                {
                    return true;
                }
                return false;
            }
        }
        _state = ButtonState.Normal;
        return false;
    }
    /// <summary>
    /// Отрисовка кнопки
    /// </summary>
    public void Draw()
    {
        GUI.DrawTexture(Rectangle, CurrentTexture);
    }
}
//ButtonState.cs
public enum ButtonState : byte
{
    Normal,
    Hover,
    Active
}
