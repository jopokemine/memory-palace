using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IronPython.Hosting;
using System;
using Microsoft.Scripting.Hosting;

public class CallingPython : MonoBehaviour {
    void Start() {
        ScriptEngine engine = Python.CreateEngine();

        ICollection<string> searchPaths = engine.GetSearchPaths();
        searchPaths.Add(Application.dataPath);
        searchPaths.Add(Application.dataPath + @"\Plugins\IronPython\Lib");
        engine.SetSearchPaths(searchPaths);

        dynamic py = engine.ExecuteFile(Application.dataPath + @"\Scripts\Python\script.py");
        dynamic greeter = py.Greeter("GenericGitHubProfile");

        Debug.Log(greeter.greet());
        Debug.Log(greeter.PythonVersion());
        Debug.Log(greeter.random_number(1,5));
    }
}
