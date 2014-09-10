using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BeastPhotoRiver
{
    /// <summary>
    /// This class is used for each Keyboard handle recognized by the InputDevice class. Because all keyboard inputs are being handled without using Microsoft's pre-built library,
    /// must distinguish Capslock, Shift, and Backspace (as well as other special keys) pressed by individual keyboards.
    /// </summary>
    /// @T.Feng 1/5/2012
    class Keyboard
    {
        //private variables
        private String _keyboardHandle;
        private String _keyboardName;

        private bool _isEnabled;
        private TextBlock _associatedCommentArea;

        private bool _capsLkOn;
        private bool _shiftPressed;
        private bool _backspacePressed;

        public Keyboard(String handle, String name)
        {
            _keyboardHandle = handle;
            _keyboardName = name;

            _isEnabled = true;//only enable Keyboard when commenting is activated
            _associatedCommentArea = null;

            //initialize as false
            _capsLkOn = false;
            _shiftPressed = false;
            _backspacePressed = false;
        }

        #region Public Properties (Handle, Name, CapsLkOn, ShiftPressed, BackspacePressed)

        public String Handle
        {
            get { return _keyboardHandle; }
        }

        public String Name
        {
            get { return _keyboardName; }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        public TextBlock AssociatedCommentArea
        {
            get { return _associatedCommentArea; }
            set { _associatedCommentArea = value; }
        }

        public bool CapsLkOn
        {
            get { return _capsLkOn; }
            set { _capsLkOn = value; }
        }

        public bool ShiftPressed
        {
            get { return _shiftPressed; }
            set { _shiftPressed = value; }
        }

        public bool BackspacePressed
        {
            get { return _backspacePressed; }
            set { _backspacePressed = value; }
        }

        #endregion
    }
}
