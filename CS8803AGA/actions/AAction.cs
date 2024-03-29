﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS8803AGAGameLibrary.actions;

namespace CS8803AGA.actions
{
    public abstract class AAction<T> : IAction
        where T : AActionInfo
    {
        protected T m_info;

        protected AAction(T info)
        {
            m_info = info;
        }

        private AAction()
        {
            // nch
        }

        #region IAction Members

        public abstract void execute(object source);

        #endregion
    }
}
