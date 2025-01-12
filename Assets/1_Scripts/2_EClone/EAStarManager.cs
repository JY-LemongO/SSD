using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class ENode
{
    public Vector2Int Pos;
    public Vector2Int ParentPos;
    public int G, H, F;
    public ENode(Vector2Int pos, Vector2Int parentPos, int g, int h)
    {
        Pos = pos;
        ParentPos = parentPos;
        G = g;
        H = h;
        F = g + h;
    }
}

public class EAStarManager : MonoBehaviour
{
    public Button SearchBtn;
    public TextMeshProUGUI OutputText;

    public Vector2Int targetPos;
    public Vector2Int currentPos;


    Vector2Int trackPos;

    Dictionary<Vector2Int, ENode> openNodes = new Dictionary<Vector2Int, ENode>();
    Dictionary<Vector2Int, ENode> closeNodes = new Dictionary<Vector2Int, ENode>();

    public List<Vector2Int> outputList;

    // 지도... 0: 지나간다, 1: 못지나간다
    public int[,] map = new int[,] // 2차원 배열
    {
        {1,1,1,1,1,1,1},
        {1,0,0,0,1,0,1},
        {1,0,0,0,1,0,1},
        {1,0,1,0,1,1,1},
        {1,0,1,0,0,0,1},
        {1,0,1,0,0,0,1},
        {1,1,1,1,1,1,1},
    };
    //1111111
    //1000101
    //1000101
    //1010101
    //1010001
    //1010001
    //1111111
    private void Start()
    {
        SearchBtn.onClick.AddListener(() => Search());
    }
    private void Search()
    {
        trackPos = currentPos;

        openNodes.Add(currentPos, new ENode(currentPos, currentPos, 0, calculateH(currentPos, targetPos)));
        Debug.Log(openNodes.Count);
        while (true)
        {
            if (openNodes.Count == 0)
            {
                Debug.Log("길을 찾을 수 없음");
                return;
            }

            // openNodes 에서, F가 가장 작은 노드 선택
            bool isInitialized = false;
            foreach (ENode node in openNodes.Values)
            {
                if (isInitialized == false)
                {
                    trackPos = node.Pos;
                    isInitialized = true;
                    continue;
                }
                if (node.F < openNodes[trackPos].F)
                    trackPos = node.Pos;
            }

            // trackPos 의 노드는 openNodes에서 제거 후 closeNodes에 추가
            Debug.Log("CLOSE : " + trackPos);
            closeNodes.Add(trackPos, openNodes[trackPos]);
            openNodes.Remove(trackPos);

            // targetPos와 trackPos가 같다면 종료 
            if (trackPos == targetPos)
            {
                Vector2Int parentPos = targetPos;
                while (parentPos != currentPos)
                {
                    outputList.Add(parentPos);
                    parentPos = closeNodes[parentPos].ParentPos;
                }
                outputList.Add(parentPos);
                outputList.Reverse();
                break;
            }

            // 대각선 및 상하좌우 openNodes에 추가
            AddToOpenNodes(trackPos.x, trackPos.y + 1);
            AddToOpenNodes(trackPos.x + 1, trackPos.y);
            AddToOpenNodes(trackPos.x, trackPos.y - 1);
            AddToOpenNodes(trackPos.x - 1, trackPos.y);
        }

        // 2차원 배열 -> 문자열 리스트
        List<string> stringList = ConvertMapToStringList(map);

        for(int i = 0; i < stringList.Count; i++)
        {
            stringList[i] = stringList[i].Replace("1", "X").Replace("0","O");
        }
        for(int i = 0; i < outputList.Count; i++)
        {
            int idxX = outputList[i].x;
            int idxY = outputList[i].y;
            // 문자열의 특정 위치 문자 변경
            stringList[idxX] = ReplaceCharAt(stringList[idxX], idxY, (i+1).ToString()[^1]);
        }

        // 문자열 리스트 -> 문자열
        string result = CombineStringList(stringList);

        OutputText.text = result;

        openNodes.Clear();
        closeNodes.Clear();

    }
    void AddToOpenNodes(int x, int y)
    {
        Vector2Int checkPos = new Vector2Int(x, y);

        try
        {
            if (map[x, y] == 1) return;
            if (closeNodes.ContainsKey(checkPos)) return;
        }
        catch { return; }

        int newCostG = closeNodes[trackPos].G + 10;

        if (openNodes.ContainsKey(checkPos)){
            if (openNodes[checkPos].G <= newCostG) return;
            Debug.Log("REPLACE : " + checkPos);
        }
        else
        {
            Debug.Log("ADD : " + checkPos);
        }
        openNodes[checkPos] = new ENode(checkPos, trackPos, closeNodes[trackPos].G + 10, calculateH(checkPos, targetPos));
    }

    int calculateH(Vector2Int curPos, Vector2Int tarPos)
    {
        return Mathf.Abs(curPos.x - tarPos.x) + Mathf.Abs(curPos.y - tarPos.y);
    }

    static List<string> ConvertMapToStringList(int[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        List<string> stringList = new List<string>();

        for (int i = 0; i < rows; i++)
        {
            char[] row = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                row[j] = map[i, j].ToString()[0];
            }
            stringList.Add(new string(row));
        }

        return stringList;
    }

    // 문자열 리스트 -> 문자열
    static string CombineStringList(List<string> stringList)
    {
        return string.Join("\n", stringList);
    }

    // 특정 문자열의 n번째 문자를 교체
    static string ReplaceCharAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}
