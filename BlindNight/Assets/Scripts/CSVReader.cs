using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class CSVReader : MonoBehaviour
{
    // Declaring varibales for the class:
    public string CSVPath = null;
    private string fileFullPath = null;

    // public  CSVfile;

    private int labelAmount, quaternionAmount, positionAmount, joints = 26;
    private string[] labelValues;

    List<Quaternion>[] quaternionDatabase;
    List<Vector3>[] positionDatabase;
    List<string> stateList;
    List<float> timestampList;

    private void Awake()
    {
        ReadCSV();
    }

    private void QuaternionInit()
    {
        quaternionAmount = joints;//((labelAmount - 1) * 4 / 7) * 1 / 4; // Calculate amount of quaternions in CSV file
        quaternionDatabase = new List<Quaternion>[quaternionAmount];


        for (int i = 0; i < quaternionAmount; i++)
        {
            quaternionDatabase[i] = new List<Quaternion>();
        }

    }

    private void PositionInit()
    {
        positionAmount = joints; // Calculate amount of position vectors in CSV file
        positionDatabase = new List<Vector3>[positionAmount];

        for (int i = 0; i < positionAmount; i++)
        {
            positionDatabase[i] = new List<Vector3>();
        }

    }

    private List<Quaternion>[] QuaternionCreator(float[] data)
    {
        int n = 0;
        for (int i = 0; i < labelValues.Length; i++)
        {
            if (labelValues[i].Contains("_Q_x"))
            {
                Quaternion tempQuaternion = new Quaternion(data[i], data[i + 1], data[i + 2], data[i + 3]);
                quaternionDatabase[n].Add(tempQuaternion);
                n++;
            }
        }
        return quaternionDatabase;
    }


    private List<Vector3>[] PositionCreator(float[] data)
    {
        int n = 0;
        for (int i = 0; i < labelValues.Length; i++)   // Vector starts from position 1
        {
            if (labelValues[i].Contains("_P_x"))
            {
                Vector3 tempPosition = new Vector3(data[i], data[i + 1], data[i + 2]);
                // Debug.Log(data[i] + " " + data[i + 1] + " " + data[i + 2]);  // For debugging (very performance heavy)
                positionDatabase[n].Add(tempPosition);
                n++;
            }
        }
        return positionDatabase;
    }

    private void StateInit()
    {
        stateList = new List<string>();
    }

    private void StateCreator(string state)
    {
        stateList.Add(state);
    }

    public void ReadCSV()
    {
        if (CSVPath != null || CSVPath != "")
        {
            fileFullPath = CSVPath;
            StreamReader strReader = new StreamReader(fileFullPath);

            bool endOfFile = false;
            bool firstRun = true;

            while (!endOfFile)
            {
                string dataString = strReader.ReadLine();

                if (dataString == null)
                {
                    endOfFile = true;
                    break;
                }

                if (firstRun)
                {
                    string[] tempLabelValues = dataString.Split(',');
                    labelAmount = tempLabelValues.Length;
                    labelValues = tempLabelValues;

                    /// Initialize the arrays and lists for quaternions, positions and timestamps:
                    QuaternionInit();
                    PositionInit();
                    StateInit();
                    TimestampInit();

                    // Debug.Log(labelValues);  // For debugging
                    firstRun = false;

                }
                else
                {
                    string[] tempDataValues = dataString.Split(',');

                    float[] dataValues = new float[tempDataValues.Length];
                    string stateValues = "";
                    for (int i = 0; i < dataValues.Length; i++)
                    {
                        if (i == dataValues.Length - 1)
                            stateValues = tempDataValues[i];
                        else
                            dataValues[i] = float.Parse(tempDataValues[i], CultureInfo.InvariantCulture.NumberFormat);

                    }

                    /// Populate the quaternion, position and timestamp arrays/lists:
                    QuaternionCreator(dataValues);
                    PositionCreator(dataValues);
                    StateCreator(stateValues);
                    Timestamp(dataValues[0]);
                }

            }
            Debug.Log("Motion Matching: pre-process completed!");
            //IndexHelper(7);     //// <- USE the index helper to find the label for a selected index in the CSV

        } else
        {
            Debug.Log("Motion Mathing ERROR: path not correct, or CSV file not found!");
        }

    }

    public void IndexHelper(int index)
    {
        Debug.Log("CSV index " + index + " is the " + labelValues[index]);     
    }

    public void IndexHelper(string all)
    {
        if (all == "all")
        {
            for (int i = 0; 0 < labelAmount; i++)
            {
                Debug.Log("CSV index " + i + " is the " + labelValues[i]);
            }

        } else
        {
            Debug.Log("Motion Matching: Index helper can only take specific index numbers or all if you want to see all joints");
        }
    }

    private void TimestampInit()
    {
        timestampList = new List<float>();
    }

    private void Timestamp(float timeStamp)
    {
        timestampList.Add(timeStamp);
    }

    public void ReadCSV(string fullPath)
    {
        fileFullPath = fullPath;
        ReadCSV();
    
    }

    /*
    public void SetFilePath(string path)
    {
        filePath = path;
    }
    public string getFilePath()
    {
        if (filePath == null)
            Debug.Log("Error: File path is null!");

        Debug.Log(filePath);
        return filePath;
    }
    public void SetFileName(string file)
    {
        fileName = file;
    }
    public string GetFileName()
    {
        if (fileName == null)
            Debug.Log("Error: File name is null!");

        Debug.Log(fileName);
        return fileName;
    } */
    public void SetFilePathFull(string fullPath)
    {
        fileFullPath = fullPath;
    }
    public string GetFilePathFull()
    {
        if (fileFullPath == null)
            Debug.Log("Error: The file path is null!");

        Debug.Log(fileFullPath);
        return fileFullPath;
    }
    public List<Quaternion>[] GetQuaternions()
    {
        if (quaternionDatabase != null)
        {
            return quaternionDatabase;
        }
        else
        {
            Debug.Log("Motion Matching Error: The quaternion database is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }   
    }

    public List<Vector3>[] GetPositions()
    {
        if (positionDatabase != null)
        {
            return positionDatabase;
        }
        else
        {
            Debug.Log("Motion Matching Error: The position database is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }
    }

    public List<string> GetStates()
    {
        if (stateList != null)
        {
            return stateList;
        }
        else
        {
            Debug.Log("Motion Matching Error: The state list is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }
    }

    public List<float> GetTimestamps()
    {
        if (timestampList != null)
        {
            return timestampList;
        }
        else
        {
            Debug.Log("Motion Matching Error: The timestamp list is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }
    }
}
