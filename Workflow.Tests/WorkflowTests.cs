using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;
using Xunit;

namespace Workflow.Tests {
    public class WorkflowTests {
        [Fact]
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

            Assert.Equal(true, result);

            Assert.Equal(null, stTrue.GetValueFromStorage());
            Assert.NotEqual(null, stFalse.GetValueFromStorage());

            doc.Reset();

            const2.Value = const1.Value;
            runner = new WfRunner(doc);
            Assert.Equal(true, runner.RunOnce());

            Assert.NotEqual(null, stTrue.GetValueFromStorage());
            Assert.Equal(null, stFalse.GetValueFromStorage());
        }

        [Fact]
        public void TestSubProgrammInputsAndOutputs() {
            WfDocument sub = new WfDocument();
            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(1, pg.Inputs.Count);
            Assert.Equal("Run", pg.Inputs[0].Name);
            Assert.Equal(0, pg.Outputs.Count);
        }

        [Fact]
        public void TestSubProgramm_1Input() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(2, pg.Inputs.Count);
            Assert.Equal("Run", pg.Inputs[0].Name);

            Assert.Equal(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.Equal(0, pg.Outputs.Count);
        }

        [Fact]
        public void TestSubProgramm_2Input() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfInputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(3, pg.Inputs.Count);
            Assert.Equal("Run", pg.Inputs[0].Name);

            Assert.Equal(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.Equal(sub.Nodes[1].Id, pg.Inputs[2].Id);
            Assert.Equal(sub.Nodes[1].Name, pg.Inputs[2].Name);
            Assert.Equal(sub.Nodes[1].GetText(), pg.Inputs[2].Text);

            Assert.Equal(0, pg.Outputs.Count);
        }

        [Fact]
        public void TestSubProgramm_2Input_Refresh_Add() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfInputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(3, pg.Inputs.Count);

            sub.Nodes.Add(new WfInputNode() { Name = "Param3" });

            pg.RefreshConnectionPoints();

            Assert.Equal(4, pg.Inputs.Count);
            Assert.Equal("Run", pg.Inputs[0].Name);

            Assert.Equal(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.Equal(sub.Nodes[1].Id, pg.Inputs[2].Id);
            Assert.Equal(sub.Nodes[1].Name, pg.Inputs[2].Name);
            Assert.Equal(sub.Nodes[1].GetText(), pg.Inputs[2].Text);

            Assert.Equal(sub.Nodes[2].Id, pg.Inputs[3].Id);
            Assert.Equal(sub.Nodes[2].Name, pg.Inputs[3].Name);
            Assert.Equal(sub.Nodes[2].GetText(), pg.Inputs[3].Text);

            Assert.Equal(0, pg.Outputs.Count);
        }

        [Fact]
        public void TestSubProgramm_3Input_Refresh_Remove() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfInputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfInputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(3, pg.Inputs.Count);

            sub.Nodes.Add(new WfInputNode() { Name = "Param3" });
            sub.Nodes.RemoveAt(1);

            pg.RefreshConnectionPoints();

            Assert.Equal(3, pg.Inputs.Count);
            Assert.Equal("Run", pg.Inputs[0].Name);

            Assert.Equal("Param3", sub.Nodes[1].Name);

            Assert.Equal(sub.Nodes[0].Id, pg.Inputs[1].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Inputs[1].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Inputs[1].Text);

            Assert.Equal(sub.Nodes[1].Id, pg.Inputs[2].Id);
            Assert.Equal(sub.Nodes[1].Name, pg.Inputs[2].Name);
            Assert.Equal(sub.Nodes[1].GetText(), pg.Inputs[2].Text);

            Assert.Equal(0, pg.Outputs.Count);
        }

        [Fact]
        public void TestSubProgramm_1Output() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(1, pg.Outputs.Count);

            Assert.Equal(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Outputs[0].Text);
        }

        [Fact]
        public void TestSubProgramm_2Output() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfOutputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(2, pg.Outputs.Count);

            Assert.Equal(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Outputs[0].Text);

            Assert.Equal(sub.Nodes[1].Id, pg.Outputs[1].Id);
            Assert.Equal(sub.Nodes[1].Name, pg.Outputs[1].Name);
            Assert.Equal(sub.Nodes[1].GetText(), pg.Outputs[1].Text);
        }

        [Fact]
        public void TestSubProgramm_2Output_Refresh_Add() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfOutputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(2, pg.Outputs.Count);

            sub.Nodes.Add(new WfOutputNode() { Name = "Param3" });

            pg.RefreshConnectionPoints();

            Assert.Equal(3, pg.Outputs.Count);

            Assert.Equal(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Outputs[0].Text);

            Assert.Equal(sub.Nodes[1].Id, pg.Outputs[1].Id);
            Assert.Equal(sub.Nodes[1].Name, pg.Outputs[1].Name);
            Assert.Equal(sub.Nodes[1].GetText(), pg.Outputs[1].Text);

            Assert.Equal(sub.Nodes[2].Id, pg.Outputs[2].Id);
            Assert.Equal(sub.Nodes[2].Name, pg.Outputs[2].Name);
            Assert.Equal(sub.Nodes[2].GetText(), pg.Outputs[2].Text);
        }

        [Fact]
        public void TestSubProgramm_3Output_Refresh_Remove() {
            WfDocument sub = new WfDocument();
            sub.Nodes.Add(new WfOutputNode() { Name = "Param1" });
            sub.Nodes.Add(new WfOutputNode() { Name = "Param2" });

            WfProgrammNode pg = new WfProgrammNode();
            pg.SubDocument = sub;

            Assert.Equal(2, pg.Outputs.Count);

            sub.Nodes.Add(new WfOutputNode() { Name = "Param3" });
            sub.Nodes.RemoveAt(1);

            pg.RefreshConnectionPoints();

            Assert.Equal(2, pg.Outputs.Count);

            Assert.Equal("Param3", sub.Nodes[1].Name);

            Assert.Equal(sub.Nodes[0].Id, pg.Outputs[0].Id);
            Assert.Equal(sub.Nodes[0].Name, pg.Outputs[0].Name);
            Assert.Equal(sub.Nodes[0].GetText(), pg.Outputs[0].Text);

            Assert.Equal(sub.Nodes[1].Id, pg.Outputs[1].Id);
            Assert.Equal(sub.Nodes[1].Name, pg.Outputs[1].Name);
            Assert.Equal(sub.Nodes[1].GetText(), pg.Outputs[1].Text);
        }

        [Fact]
        public void TestRepeat() {
            WfDocument doc = new WfDocument();

            WfCustomNode node1 = new WfCustomNode((r, n) => { return true; });
            node1.Name = "Database";
            WfCustomNode node2 = new WfCustomNode((r, n) => { return true; });
            node2.Name = "Table";
            doc.AddNode(node1);
            doc.AddNode(node2);
            node1.Connect("Out", node2, "In");

            WfRepeatNode repeatNode = new WfRepeatNode();
            doc.AddNode(repeatNode);
            repeatNode.Count = 20;
            repeatNode.Name = "Repeat";
            WfCustomNode timerNode = new WfCustomNode((r, n) => { return true; });
            doc.AddNode(timerNode);
            timerNode.Name = "Timer";
            repeatNode.Connect("Repeat", timerNode, "In");
            WfCustomNode randomNode = new WfCustomNode((r, n) => { return true; });
            randomNode.Name = "Random";
            doc.AddNode(randomNode);
            timerNode.Connect("Out", randomNode, "In");

            int count = 0;
            WfCustomNode groupNode = new WfCustomNode((r, n) => {
                count++;
                return true; }
            );
            doc.AddNode(groupNode);
            groupNode.Inputs.Add(new WfConnectionPoint() { Name = "Item0", Text = "Item0", Requirement = WfRequirementType.Optional });
            groupNode.Inputs.Add(new WfConnectionPoint() { Name = "Item1", Text = "Item1", Requirement = WfRequirementType.Optional });
            groupNode.Name = "Insert";
            randomNode.Connect("Out", groupNode, "Item0");
            node2.Connect("Out", groupNode, "Item1");

            WfRunner runner = new WfRunner(doc);
            runner.Initialize();

            Assert.Equal(repeatNode, groupNode.ScopeRoot);
            Assert.Equal(repeatNode, timerNode.ScopeRoot);
            Assert.Equal(repeatNode, randomNode.ScopeRoot);
            Assert.Null(node1.ScopeRoot);
            Assert.Null(node2.ScopeRoot);

            runner.RunOnce();
            Assert.Equal(20, count);
        }
    }
}
