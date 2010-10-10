using System;
using System.Linq;
using NUnit.Framework;

namespace DeanIS.Framework.Testing
{
    [TestFixture]
    public abstract class context
    {
        private Exception exception;

        [SetUp]
        public virtual void initialize()
        {
            exception = null;
            setupContext();
            extendContext();

            try
            {
                act();
            }
            catch (Exception e)
            {
                exception = e;
                if (ShouldRethrowException(e))
                {
                    throw;
                }
            }
            finally
            {
                postact();
            }
        }

        public Exception CaughtException
        {
            get { return exception; }
        }

        public abstract void setupContext();
        public abstract void act();
        public virtual void postact() { }
        public virtual void extendContext() { }

        protected virtual bool ShouldRethrowException(Exception e)
        {
            var exceptionType = e.GetType();

            var attribute = GetExpectedExceptionAttribute();

            return attribute == null || attribute.ExceptionType != null && attribute.ExceptionType != exceptionType;
        }

        // This will never work because you can't place the ExpectedExcpetion attribute on a class
        // (this.GetType()) and you can't target the method that's acually being called easily.
        private ContextExpectedExceptionAttribute GetExpectedExceptionAttribute()
        {
            return GetType().GetCustomAttributes(true).OfType<ContextExpectedExceptionAttribute>().FirstOrDefault();
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    internal sealed class ContextExpectedExceptionAttribute : Attribute
    {
        public Type ExceptionType { get; set; }

    }
}