﻿using System.Collections.Generic;

namespace Fusee.Base.Common
{
    /// <summary>
    /// Interface describing what operations are possible on arbitrary image data
    /// </summary>
    public interface IImageData
    {
        /// <summary>
        /// Width of the IImageData object in pixels.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Height of the IImageData object in pixels.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// PixelFormat contains additional information about IImageData's pixel encoding, e.g. amount of color channels.
        /// </summary>
        ImagePixelFormat PixelFormat { get; }

        /// <summary>
        /// Block Image Transfer. Write a block of pixels to this instance from some other IImageData
        /// </summary>
        /// <param name="xDst">x offset (pixels) of destination.</param>
        /// <param name="yDst">y offset (pixels) of destination.</param>
        /// <param name="src">The source IImageData instance. The pixel Block will be copied from here.</param>
        /// <param name="xSrc">x offset (pixels) of source.</param>
        /// <param name="ySrc">y offset (pixels) of source.</param>
        /// <param name="width">Width of the pixel block in pixels.</param>
        /// <param name="height">Height of the pixel block in pixels.</param>
        void Blt(int xDst, int yDst, IImageData src, int xSrc = 0, int ySrc = 0, int width = 0, int height = 0);

        /// <summary>
        /// Expose a set of pixel lines (enables IImageData to be used as src in other instances' Blt)
        /// </summary>
        /// <param name="xSrc">x offset (pixels) of source.</param>
        /// <param name="ySrc">y offset (pixels) of source.</param>
        /// <param name="width">Width of all Scanlines in pixels.</param>
        /// <param name="height">Height of all Scanlines in pixels.</param>
        /// <returns></returns>
        IEnumerator<ScanLine> ScanLines(int xSrc = 0, int ySrc = 0, int width = 0, int height = 0);

        /// <summary>
        /// Returns true if IImageData has not been initialized with an byte array.
        /// </summary>
        bool IsEmpty { get; }


    }
}