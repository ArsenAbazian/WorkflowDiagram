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
            doc.AddNode(const1);

            WfConstantValueNode const2 = new WfConstantValueNode(2.0);
            doc.AddNode(const2);

            WfConditionNode cond1 = new WfConditionNode(WfConditionalOperation.Equal);
            doc.AddNode(cond1);

            const1.Outputs[0].ConnectTo(cond1, "In1");
            const2.Outputs[0].ConnectTo(cond1, "In2");

            WfStorageValueNode stTrue = new WfStorageValueNode("Condition True");
            doc.AddNode(stTrue);

            WfStorageValueNode stFalse = new WfStorageValueNode("Condition False");
            doc.AddNode(stFalse);

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

        [Test]
        public void TestSubProgrammInputsAndOutputs() {
            WfDocument sub = new WfDocument();
            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(1, pg.Inputs.Count);
            Assert.AreEqual("Run", pg.Inputs[0].Name);
            Assert.AreEqual(0, pg.Outputs.Count);
        }

        [Test]
        public void TestSubProgramm_1Input() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(2, pg.Inputs.Count);
            Assert.AreEqual("Run", pg.Inputs[0].Name);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.AreEqual(0, pg.Outputs.Count);
        }

        [Test]
        public void TestSubProgramm_2Input() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfInputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(3, pg.Inputs.Count);
            Assert.AreEqual("Run", pg.Inputs[0].Name);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.AreEqual(sub.Nodes[1].Id, pg.Inputs[2].Id);
            Assert.AreEqual(sub.Nodes[1].Name, pg.Inputs[2].Name);
            Assert.AreEqual(sub.Nodes[1].GetText(), pg.Inputs[2].Text);

            Assert.AreEqual(0, pg.Outputs.Count);
        }

        [Test]
        public void TestSubProgramm_2Input_Refresh_Add() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfInputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(3, pg.Inputs.Count);

            sub.Nodes.Add(new WfInputNode() { Name = "Param3" });

            pg.RefreshConnectionPoints();

            Assert.AreEqual(4, pg.Inputs.Count);
            Assert.AreEqual("Run", pg.Inputs[0].Name);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.AreEqual(sub.Nodes[1].Id, pg.Inputs[2].Id);
            Assert.AreEqual(sub.Nodes[1].Name, pg.Inputs[2].Name);
            Assert.AreEqual(sub.Nodes[1].GetText(), pg.Inputs[2].Text);

            Assert.AreEqual(sub.Nodes[2].Id, pg.Inputs[3].Id);
            Assert.AreEqual(sub.Nodes[2].Name, pg.Inputs[3].Name);
            Assert.AreEqual(sub.Nodes[2].GetText(), pg.Inputs[3].Text);

            Assert.AreEqual(0, pg.Outputs.Count);
        }

        [Test]
        public void TestSubProgramm_3Input_Refresh_Remove() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfInputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(3, pg.Inputs.Count);

            sub.Nodes.Add(new WfInputNode() { Name = "Param3" });
            sub.Nodes.RemoveAt(1);

            pg.RefreshConnectionPoints();

            Assert.AreEqual(3, pg.Inputs.Count);
            Assert.AreEqual("Run", pg.Inputs[0].Name);

            Assert.AreEqual("Param3", sub.Nodes[1].Name);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.AreEqual(sub.Nodes[1].Id, pg.Inputs[2].Id);
            Assert.AreEqual(sub.Nodes[1].Name, pg.Inputs[2].Name);
            Assert.AreEqual(sub.Nodes[1].GetText(), pg.Inputs[2].Text);

            Assert.AreEqual(0, pg.Outputs.Count);
        }

        [Test]
        public void TestSubProgramm_1Output() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(1, pg.Outputs.Count);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Outputs[0].Text);
        }

        [Test]
        public void TestSubProgramm_2Output() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfOutputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(2, pg.Outputs.Count);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Outputs[0].Text);

            Assert.AreEqual(sub.Nodes[1].Id, pg.Outputs[1].Id);
            Assert.AreEqual(sub.Nodes[1].Name, pg.Outputs[1].Name);
            Assert.AreEqual(sub.Nodes[1].GetText(), pg.Outputs[1].Text);
        }

        [Test]
        public void TestSubProgramm_2Output_Refresh_Add() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfOutputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(2, pg.Outputs.Count);

            sub.Nodes.Add(new WfOutputNode() { Name = "Param3" });

            pg.RefreshConnectionPoints();

            Assert.AreEqual(3, pg.Outputs.Count);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Outputs[0].Text);

            Assert.AreEqual(sub.Nodes[1].Id, pg.Outputs[1].Id);
            Assert.AreEqual(sub.Nodes[1].Name, pg.Outputs[1].Name);
            Assert.AreEqual(sub.Nodes[1].GetText(), pg.Outputs[1].Text);

            Assert.AreEqual(sub.Nodes[2].Id, pg.Outputs[2].Id);
            Assert.AreEqual(sub.Nodes[2].Name, pg.Outputs[2].Name);
            Assert.AreEqual(sub.Nodes[2].GetText(), pg.Outputs[2].Text);
        }

        [Test]
        public void TestSubProgramm_3Output_Refresh_Remove() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfOutputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.AreEqual(2, pg.Outputs.Count);

            sub.Nodes.Add(new WfOutputNode() { Name = "Param3" });
            sub.Nodes.RemoveAt(1);

            pg.RefreshConnectionPoints();

            Assert.AreEqual(2, pg.Outputs.Count);

            Assert.AreEqual("Param3", sub.Nodes[1].Name);

            Assert.AreEqual(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.AreEqual(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.AreEqual(sub.Nodes[0].GetText(), pg.Outputs[0].Text);

            Assert.AreEqual(sub.Nodes[1].Id, pg.Outputs[1].Id);
            Assert.AreEqual(sub.Nodes[1].Name, pg.Outputs[1].Name);
            Assert.AreEqual(sub.Nodes[1].GetText(), pg.Outputs[1].Text);
        }
    }
}
