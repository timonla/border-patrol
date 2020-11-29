using System;
using System.Collections.Generic;
using System.Text;
using Scripts.WalkerStructure;

namespace BorderPatrol.Tests.Walker {
    class Helpers {
        class TagContainer {
            public int xValue;
            public int yValue;
            public string renderOutput;
            public bool running;

            public TagContainer() {
                xValue = 0;
                yValue = 0;
                renderOutput = "";
                running = false;
            }
        }

        public static WalkerTags CreateTags() {
            var tagContainer = new TagContainer();

            return new WalkerTags(
                xValue: new VariableReference<int>(
                    () => tagContainer.xValue,
                    val => { tagContainer.xValue = val; }),
                yValue: new VariableReference<int>(
                    () => tagContainer.yValue,
                    val => { tagContainer.yValue = val; }),
                renderOutput: new VariableReference<string>(
                    () => tagContainer.renderOutput,
                    val => { tagContainer.renderOutput = val; }),
                running: new VariableReference<bool>(
                    () => tagContainer.running,
                    val => { tagContainer.running = val; })
            );
        }
    }
}
