using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class Try
    {
        private Exception e;
        private bool failed = false;

        public Try of(Action a)
        {
            try
            {
                a.Invoke();
                failed = false;
            }
            catch (Exception ex)
            {
                this.failed = true;
                e = ex;
            }

            return this;
        }

        public Exception getException()
        {
            return e;
        }

        public bool executedGracefully()
        {
            return failed;
        }

        public Try or(Action a)
        {
            a.Invoke();
            return this;
        }

        public Try afterSuccess(Action a)
        {
            if (!failed) a.Invoke();
            return this;
        }

        public Try afterFail(Action a)
        {
            if (failed) a.Invoke();
            return this;
        }
    }
}