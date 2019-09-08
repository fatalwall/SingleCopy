/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */

namespace SingleCopy.Plugin
{
    public interface IPostScanAction
    {
        void Action();
    }

    public interface IPostScanActionMetadata
    {
        string Name { get; }
    }
}
