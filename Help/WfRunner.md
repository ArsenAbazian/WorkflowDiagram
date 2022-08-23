## WfRunner class

The WfRunner class allows you to initialize and execute your workflow diagram. Here is the quick example: 

```csharp
    WfDocument doc = new WfDocument(); 
    WfRunner runner = new WfRunner(doc);
    bool result = runner.RunOnce(doc);
```