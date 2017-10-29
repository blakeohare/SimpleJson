# SimpleJson
Simple JSON parser for C#

To use the parser for your project, simply copy the JsonParser.cs file into your project.

There are several benefits to SimpleJson over common alternatives:

* This is completely public domain.
* This only uses raw C# with no dependencies.
* Unlike other third party libraries, this won't add more DLL clutter to your final binary.
* The current "built in" method for parsing JSON requires including a System.Web assembly extension that isn't available in all versions of .NET.
* By default, SimpleJson will follow the strict JSON standard, but has several flags to toggle various leniencies on or off.
* Order of keys from the JSON is always preserved in the Key collection iterator. Generally this shouldn't matter strictly following the standard, but there are certain edge cases where it is important.

If you run this .sln project as-is, it is essentially just a unit test harness.

## Sample usage:

```csharp
using SimpleJson;

...

IDictionary<string, object> result = new JsonParser(myRawJsonText)
    .AddOption(JsonOption.ALLOW_COMMENTS) // allow C style /* comments */
    .AddOption(JsonOption.ALLOW_DECIMAL_AS_FLOAT_FIRST_CHAR) // enable .123 format for floats, instead of only 0.123
    .AddOption(JsonOption.OVERWRITE_DUPLICATE_KEYS) // Don't throw error on duplicate key
    .ParseAsDictionary();

...
```

The type of the dictionary value is generally the canonical C# type. Booleans, integers, strings, and nulls in the JSON will appear likewise in the resulting C# dictionary. JSON floats will be converted into C# doubles. JSON lists will be `object[]`, and JSON objects will be `IDictionary<string, object>`. Note that this is an `IDictionary` and not a regular `Dictionary`.

## Alternate Usage:

You can also use SimpleJson to parse similar formats. For example, if you have a string format that is a bunch of deeply nested lists (such as a matrix), you can add the `JsonOption.ALLOW_NON_OBJECT_AS_ROOT` option to parse things like `[[1, 2, 3], [4, 5, 6], [7, 8, 9]]`.

## JsonOptions

This is the full list of parser options:

| Enum Name | Effects |
| --- | --- |
| FAIL_SILENTLY | Do not throw an exception on an error. Simply return null instead. |
| ALLOW_COMMENTS | This will allow C-style multiline comment syntax. |
| ALLOW_TRAILING_COMMA | This will allow commas after the last item of a dictionary or list. |
| ALLOW_SINGLE_QUOTE | This will allow single quotes |
| ALLOW_NON_QUOTED_KEYS | This will allow object keys that are not in quotes, like JavaScript. |
| ALLOW_NON_OBJECT_AS_ROOT | This allows the root element to be something other than an object type. See *Alternate Usage* subheading. |
| ALLOW_DECIMAL_AS_FLOAT_FIRST_CHAR | This allows float values that begin with a decimal. Generally strict JSON will not allow leading decimals without a 0 prefix. |
| OVERWRITE_DUPLICATE_KEYS | Enabling this will make duplicate keys override the previous value. Otherwise duplicate keys will be an error. |

