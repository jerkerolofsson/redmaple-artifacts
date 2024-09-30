using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMaple.Artifacts.Contracts
{
    public class SemverVersion : IVersion
    {
        private static int MAJOR_INDEX = 0;
        private static int MINOR_INDEX = 1;
        private static int PATCH_INDEX = 2;
        private static int MULTIPLIER = 10000;

        public string[] Components { get; set; }

        public long SerializedVersion
        {
            get
            {
                if (!long.TryParse(Components[0], out var a))
                {
                    throw new InvalidDataException("Expected integer");
                }
                if (!long.TryParse(Components[1], out var b))
                {
                    throw new InvalidDataException("Expected integer");
                }
                if (!long.TryParse(Components[2], out var c))
                {
                    throw new InvalidDataException("Expected integer");
                }

                return a * MULTIPLIER * MULTIPLIER + b * MULTIPLIER + c;
            }
        }
        public string VersionString
        {
            get
            {
                var sb = new StringBuilder();
                for(int i=0; i<Components.Length; i++)
                {
                    if(i>0)
                    {
                        if(i==1 || i==2)
                        {
                            sb.Append('.');
                        }
                        else
                        {
                            sb.Append('-');
                        }
                    }
                    sb.Append(Components[i]);
                }
                return sb.ToString();
            }
        }

        public SemverVersion()
        {
            Components = ["0", "0", "0"];
        }
        public SemverVersion(string[] components)
        {
            Components = components;
            ThrowIfInvalid();
        }

        private void ThrowIfInvalid()
        {
            if (Components.Length < 3)
            {
                throw new ArgumentException("Invalid semver version. Length must be atleast 3");
            }

            string indexError = "Invalid semver version. Expected integer at index {index}";
            if (!long.TryParse(Components[0], out var _))
            {
                throw new ArgumentException(string.Format(indexError, 0));
            }
            if (!long.TryParse(Components[1], out var _))
            {
                throw new ArgumentException(string.Format(indexError, 1));
            }
            if (!long.TryParse(Components[2], out var _))
            {
                throw new ArgumentException(string.Format(indexError, 2));
            }
        }

        public static SemverVersion Parse(string text)
        {
            return new SemverVersion(text.Split('.', '-'));
        }

        public IVersion IncrementMajor()
        {
            return Increment(index: MAJOR_INDEX);
        }
        public IVersion IncrementMinor()
        {
            return Increment(index: MINOR_INDEX);
        }
        public IVersion IncrementPatch()
        {
            return Increment(index: PATCH_INDEX);
        }

        public SemverVersion Increment(int index)
        {
            if(index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if(Components.Length < index)
            {
                throw new InvalidDataException("Cannot increment version component as the component count is insufficient");
            }
            if (int.TryParse(Components[index], out var count))
            {
                var components = new string[Components.Length];
                Array.Copy(Components, components, Components.Length);

                count++;

                // Max supported is 9999
                if(count >= 10_000 && index > 0)
                {
                    return Increment(index - 1);
                }

                components[index] = (count + 1).ToString();
                for(int i=index+1; i<3;i++)
                {
                    components[i] = "0";
                }
                return new SemverVersion(components);
            }
            else
            {
                throw new InvalidDataException("Cannot increment version component as the component at the specified index is not a valid integer");
            }
        }
    }
}
