using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace Workflow.Tests {
    [TestFixture]
    public class WorkflowTests {
        [Test]
        public void TestConditionalExecution() {
            WfDocument doc = new WfDocument();

            WfConstantValueNode const1 = new WfConstantValueNode(1.0);
            doc.Add(const1);

            WfConstantValueNode const2 = new WfConstantValueNode(2.0);
            doc.Add(const2);

            WfConditionNode cond1 = new WfConditionNode(WfConditionalOperation.Equal);
            doc.Add(cond1);

            const1.Outputs[0].ConnectTo(cond1, "In1");
            const2.Outputs[0].ConnectTo(cond1, "In2");

            WfStorageValueNode stTrue = new WfStorageValueNode("Condition True");
            doc.Add(stTrue);

            WfStorageValueNode stFalse = new WfStorageValueNode("Condition False");
            doc.Add(stFalse);

            cond1.Connect("False", stFalse, "Run");
            cond1.Connect("True", stTrue, "Run");

            WfRunner runner = new WfRunner(doc);
            bool result = runner.RunOnce();

            Assert.AreEqual(true, result);

            Assert.AreEqual(null, stTrue.GetValueFromStorage());
            Assert.AreNotEqual(null, stFalse.GetValueFromStorage());
            
            doc.Reset();

            const2.Value = const1.Value;
            runner = new WfRunner(doc);
            Assert.AreEqual(true, runner.RunOnce());

            Assert.AreNotEqual(null, stTrue.GetValueFromStorage());
            Assert.AreEqual(null, stFalse.GetValueFromStorage());
        }
    }
}
