﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILateTick
{
    void OnLateTick();

    bool Process { get; }
}
