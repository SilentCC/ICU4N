﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using ICU4N.Impl;
using ICU4N.Support.Text;
using System;
using System.Text;

namespace ICU4N.Text
{
    public sealed partial class SimpleFormatter
    {

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax.</exception>
        /// <stable>ICU 57</stable>
        public static SimpleFormatter Compile(string pattern)
        {
            return CompileMinMaxArguments(pattern, 0, int.MaxValue);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax.</exception>
        /// <stable>ICU 57</stable>
        public static SimpleFormatter Compile(StringBuilder pattern)
        {
            return CompileMinMaxArguments(pattern, 0, int.MaxValue);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax.</exception>
        /// <stable>ICU 57</stable>
        public static SimpleFormatter Compile(char[] pattern)
        {
            return CompileMinMaxArguments(pattern, 0, int.MaxValue);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax.</exception>
        /// <stable>ICU 57</stable>
        internal static SimpleFormatter Compile(ICharSequence pattern)
        {
            return CompileMinMaxArguments(pattern, 0, int.MaxValue);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// The number of arguments checked against the given limits is the
        /// highest argument number plus one, not the number of occurrences of arguments.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <param name="min">The pattern must have at least this many arguments.</param>
        /// <param name="max">The pattern must have at most this many arguments.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax and too few or too many arguments.</exception>
        /// <stable>ICU 57</stable>
        public static SimpleFormatter CompileMinMaxArguments(string pattern, int min, int max)
        {
            StringBuilder sb = new StringBuilder();
            string compiledPattern = SimpleFormatterImpl.CompileToStringMinMaxArguments(pattern, sb, min, max);
            return new SimpleFormatter(compiledPattern);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// The number of arguments checked against the given limits is the
        /// highest argument number plus one, not the number of occurrences of arguments.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <param name="min">The pattern must have at least this many arguments.</param>
        /// <param name="max">The pattern must have at most this many arguments.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax and too few or too many arguments.</exception>
        /// <stable>ICU 57</stable>
        public static SimpleFormatter CompileMinMaxArguments(StringBuilder pattern, int min, int max)
        {
            StringBuilder sb = new StringBuilder();
            string compiledPattern = SimpleFormatterImpl.CompileToStringMinMaxArguments(pattern, sb, min, max);
            return new SimpleFormatter(compiledPattern);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// The number of arguments checked against the given limits is the
        /// highest argument number plus one, not the number of occurrences of arguments.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <param name="min">The pattern must have at least this many arguments.</param>
        /// <param name="max">The pattern must have at most this many arguments.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax and too few or too many arguments.</exception>
        /// <stable>ICU 57</stable>
        public static SimpleFormatter CompileMinMaxArguments(char[] pattern, int min, int max)
        {
            StringBuilder sb = new StringBuilder();
            string compiledPattern = SimpleFormatterImpl.CompileToStringMinMaxArguments(pattern, sb, min, max);
            return new SimpleFormatter(compiledPattern);
        }

        /// <summary>
        /// Creates a formatter from the pattern string.
        /// The number of arguments checked against the given limits is the
        /// highest argument number plus one, not the number of occurrences of arguments.
        /// </summary>
        /// <param name="pattern">The pattern string.</param>
        /// <param name="min">The pattern must have at least this many arguments.</param>
        /// <param name="max">The pattern must have at most this many arguments.</param>
        /// <returns>The new <see cref="SimpleFormatter"/> object.</returns>
        /// <exception cref="ArgumentException">For bad argument syntax and too few or too many arguments.</exception>
        /// <stable>ICU 57</stable>
        internal static SimpleFormatter CompileMinMaxArguments(ICharSequence pattern, int min, int max)
        {
            StringBuilder sb = new StringBuilder();
            string compiledPattern = SimpleFormatterImpl.CompileToStringMinMaxArguments(pattern, sb, min, max);
            return new SimpleFormatter(compiledPattern);
        }

        /// <summary>
        /// Formats the given values.
        /// </summary>
        /// <stable>ICU 57</stable>
        public string Format(params string[] values)
        {
            return SimpleFormatterImpl.FormatCompiledPattern(compiledPattern, values);
        }

        /// <summary>
        /// Formats the given values.
        /// </summary>
        /// <stable>ICU 57</stable>
        public string Format(params StringBuilder[] values)
        {
            return SimpleFormatterImpl.FormatCompiledPattern(compiledPattern, values);
        }

        /// <summary>
        /// Formats the given values.
        /// </summary>
        /// <stable>ICU 57</stable>
		[CLSCompliant(false)]
        public string Format(params char[][] values)
        {
            return SimpleFormatterImpl.FormatCompiledPattern(compiledPattern, values);
        }

        /// <summary>
        /// Formats the given values.
        /// </summary>
        /// <stable>ICU 57</stable>
        internal string Format(params ICharSequence[] values)
        {
            return SimpleFormatterImpl.FormatCompiledPattern(compiledPattern, values);
        }

        /// <summary>
        /// Formats the given values, appending to the <paramref name="appendTo"/> builder.
        /// </summary>
        /// <param name="appendTo">Gets the formatted pattern and values appended.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value must not be the same object as appendTo.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// Can be null if <see cref="ArgumentLimit"/>==0.
        /// </param>
        /// <returns><paramref name="appendTo"/></returns>
        /// <stable>ICU 57</stable>
        public StringBuilder FormatAndAppend(
            StringBuilder appendTo, int[] offsets, params string[] values)
        {
            return SimpleFormatterImpl.FormatAndAppend(compiledPattern, appendTo, offsets, values);
        }

        /// <summary>
        /// Formats the given values, appending to the <paramref name="appendTo"/> builder.
        /// </summary>
        /// <param name="appendTo">Gets the formatted pattern and values appended.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value must not be the same object as appendTo.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// Can be null if <see cref="ArgumentLimit"/>==0.
        /// </param>
        /// <returns><paramref name="appendTo"/></returns>
        /// <stable>ICU 57</stable>
        public StringBuilder FormatAndAppend(
            StringBuilder appendTo, int[] offsets, params StringBuilder[] values)
        {
            return SimpleFormatterImpl.FormatAndAppend(compiledPattern, appendTo, offsets, values);
        }

        /// <summary>
        /// Formats the given values, appending to the <paramref name="appendTo"/> builder.
        /// </summary>
        /// <param name="appendTo">Gets the formatted pattern and values appended.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value must not be the same object as appendTo.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// Can be null if <see cref="ArgumentLimit"/>==0.
        /// </param>
        /// <returns><paramref name="appendTo"/></returns>
        /// <stable>ICU 57</stable>
		[CLSCompliant(false)]
        public StringBuilder FormatAndAppend(
            StringBuilder appendTo, int[] offsets, params char[][] values)
        {
            return SimpleFormatterImpl.FormatAndAppend(compiledPattern, appendTo, offsets, values);
        }

        /// <summary>
        /// Formats the given values, appending to the <paramref name="appendTo"/> builder.
        /// </summary>
        /// <param name="appendTo">Gets the formatted pattern and values appended.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value must not be the same object as appendTo.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// Can be null if <see cref="ArgumentLimit"/>==0.
        /// </param>
        /// <returns><paramref name="appendTo"/></returns>
        /// <stable>ICU 57</stable>
        internal StringBuilder FormatAndAppend(
            StringBuilder appendTo, int[] offsets, params ICharSequence[] values)
        {
            return SimpleFormatterImpl.FormatAndAppend(compiledPattern, appendTo, offsets, values);
        }

        /// <summary>
        /// Formats the given values, replacing the contents of the result builder.
        /// May optimize by actually appending to the result if it is the same object
        /// as the value corresponding to the initial argument in the pattern.
        /// </summary>
        /// <param name="result">Gets its contents replaced by the formatted pattern and values.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value may be the same object as result.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// </param>
        /// <returns><paramref name="result"/></returns>
        /// <stable>ICU 57</stable>
        public StringBuilder FormatAndReplace(
            StringBuilder result, int[] offsets, params string[] values)
        {
            return SimpleFormatterImpl.FormatAndReplace(compiledPattern, result, offsets, values);
        }

        /// <summary>
        /// Formats the given values, replacing the contents of the result builder.
        /// May optimize by actually appending to the result if it is the same object
        /// as the value corresponding to the initial argument in the pattern.
        /// </summary>
        /// <param name="result">Gets its contents replaced by the formatted pattern and values.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value may be the same object as result.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// </param>
        /// <returns><paramref name="result"/></returns>
        /// <stable>ICU 57</stable>
        public StringBuilder FormatAndReplace(
            StringBuilder result, int[] offsets, params StringBuilder[] values)
        {
            return SimpleFormatterImpl.FormatAndReplace(compiledPattern, result, offsets, values);
        }

        /// <summary>
        /// Formats the given values, replacing the contents of the result builder.
        /// May optimize by actually appending to the result if it is the same object
        /// as the value corresponding to the initial argument in the pattern.
        /// </summary>
        /// <param name="result">Gets its contents replaced by the formatted pattern and values.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value may be the same object as result.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// </param>
        /// <returns><paramref name="result"/></returns>
        /// <stable>ICU 57</stable>
		[CLSCompliant(false)]
        public StringBuilder FormatAndReplace(
            StringBuilder result, int[] offsets, params char[][] values)
        {
            return SimpleFormatterImpl.FormatAndReplace(compiledPattern, result, offsets, values);
        }

        /// <summary>
        /// Formats the given values, replacing the contents of the result builder.
        /// May optimize by actually appending to the result if it is the same object
        /// as the value corresponding to the initial argument in the pattern.
        /// </summary>
        /// <param name="result">Gets its contents replaced by the formatted pattern and values.</param>
        /// <param name="offsets">
        /// offsets[i] receives the offset of where
        /// values[i] replaced pattern argument {i}.
        /// Can be null, or can be shorter or longer than values.
        /// If there is no {i} in the pattern, then offsets[i] is set to -1.
        /// </param>
        /// <param name="values">
        /// The argument values.
        /// An argument value may be the same object as result.
        /// values.Length must be at least <see cref="ArgumentLimit"/>.
        /// </param>
        /// <returns><paramref name="result"/></returns>
        /// <stable>ICU 57</stable>
        internal StringBuilder FormatAndReplace(
            StringBuilder result, int[] offsets, params ICharSequence[] values)
        {
            return SimpleFormatterImpl.FormatAndReplace(compiledPattern, result, offsets, values);
        }

	}
}