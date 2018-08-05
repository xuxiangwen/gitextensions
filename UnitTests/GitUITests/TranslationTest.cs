﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GitUI;
using NUnit.Framework;
using ResourceManager;
using ResourceManager.Xliff;

namespace GitUITests
{
    [TestFixture]
    public class TranslationTest
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void CreateInstanceOfClass()
        {
            // just reference to GitUI
            MouseWheelRedirector.Active = true;

            var translatableTypes = TranslationUtil.GetTranslatableTypes();

            var problems = new List<(string typeName, Exception exception)>();

            foreach (var types in translatableTypes.Values)
            {
                var translation = new TranslationFile();

                foreach (var type in types)
                {
                    try
                    {
                        var obj = (ITranslate)TranslationUtil.CreateInstanceOfClass(type);

                        obj.AddTranslationItems(translation);
                        obj.TranslateItems(translation);
                    }
                    catch (Exception ex)
                    {
                        problems.Add((type.FullName, ex));
                    }
                }
            }

            if (problems.Count != 0)
            {
                Assert.Fail(string.Join(
                    "\n\n--------\n\n",
                    problems.Select(p => $"Problem with type {p.typeName}\n\n{p.exception}")));
            }
        }
    }
}
