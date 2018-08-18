/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPoleMover : MonoBehaviour
{
    private bool shouldMove;
    List<PoleInfo> poleInfoList = new List<PoleInfo>();

    private class PoleInfo
    {
        public Transform transform;
        public Vector3 startPosition;
        public bool movingUp;
    }

    // Use this for initialization
    void Start()
    {
        //Gather all poles and put them in the list
        for (int i = 0; i < transform.childCount; i++)
        {
            PoleInfo info = new PoleInfo();
            info.transform = transform.GetChild(i);
            info.startPosition = transform.GetChild(i).localPosition;
            info.movingUp = true;

            poleInfoList.Add(info);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            for (int i = 0; i < poleInfoList.Count; i++)
            {
                PoleInfo currentPole = poleInfoList[i];

                float positionDifference = currentPole.transform.localPosition.y - currentPole.startPosition.y;

                if (currentPole.movingUp && positionDifference > 5f)
                {
                    currentPole.movingUp = false;
                }
                else if (!currentPole.movingUp && positionDifference < -5f)
                {
                    currentPole.movingUp = true;
                }

                if (currentPole.movingUp)
                {
                    currentPole.transform.Translate(Vector3.up * Time.deltaTime * .5f);
                }
                else
                {
                    currentPole.transform.Translate(Vector3.up * Time.deltaTime * -.5f);
                }

            }
        }
    }

    public void EnableMoving()
    {
        shouldMove = true;
    }

    public void DisableMoving()
    {
        shouldMove = false;
    }

}
