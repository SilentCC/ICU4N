﻿using ICU4N.Lang;
using ICU4N.Support.Collections;
using ICU4N.Support.Text;
using ICU4N.Text;
using NUnit.Framework;
using System;
using ScriptUsage = ICU4N.Lang.UScript.ScriptUsage; // ICU4N TODO: De-nest ?

namespace ICU4N.Dev.Test.Lang
{
    public class TestUScript : TestFmwk
    {
        /**
    * Constructor
    */
        public TestUScript()
        {
        }

        [Test]
        public void TestGetScriptOfCharsWithScriptExtensions()
        {
            /* test characters which have Script_Extensions */
            if (!(
                UScript.COMMON == UScript.GetScript(0x0640) &&
                UScript.INHERITED == UScript.GetScript(0x0650) &&
                UScript.ARABIC == UScript.GetScript(0xfdf2))
            )
            {
                Errln("UScript.getScript(character with Script_Extensions) failed");
            }
        }

        [Test]
        public void TestHasScript()
        {
            if (!(
                !UScript.HasScript(0x063f, UScript.COMMON) &&
                UScript.HasScript(0x063f, UScript.ARABIC) &&  /* main Script value */
                !UScript.HasScript(0x063f, UScript.SYRIAC) &&
                !UScript.HasScript(0x063f, UScript.THAANA))
            )
            {
                Errln("UScript.hasScript(U+063F, ...) is wrong");
            }
            if (!(
                !UScript.HasScript(0x0640, UScript.COMMON) &&  /* main Script value */
                UScript.HasScript(0x0640, UScript.ARABIC) &&
                UScript.HasScript(0x0640, UScript.SYRIAC) &&
                !UScript.HasScript(0x0640, UScript.THAANA))
            )
            {
                Errln("UScript.hasScript(U+0640, ...) is wrong");
            }
            if (!(
                !UScript.HasScript(0x0650, UScript.INHERITED) &&  /* main Script value */
                UScript.HasScript(0x0650, UScript.ARABIC) &&
                UScript.HasScript(0x0650, UScript.SYRIAC) &&
                !UScript.HasScript(0x0650, UScript.THAANA))
            )
            {
                Errln("UScript.hasScript(U+0650, ...) is wrong");
            }
            if (!(
                !UScript.HasScript(0x0660, UScript.COMMON) &&  /* main Script value */
                UScript.HasScript(0x0660, UScript.ARABIC) &&
                !UScript.HasScript(0x0660, UScript.SYRIAC) &&
                UScript.HasScript(0x0660, UScript.THAANA))
            )
            {
                Errln("UScript.hasScript(U+0660, ...) is wrong");
            }
            if (!(
                !UScript.HasScript(0xfdf2, UScript.COMMON) &&
                UScript.HasScript(0xfdf2, UScript.ARABIC) &&  /* main Script value */
                !UScript.HasScript(0xfdf2, UScript.SYRIAC) &&
                UScript.HasScript(0xfdf2, UScript.THAANA))
            )
            {
                Errln("UScript.hasScript(U+FDF2, ...) is wrong");
            }
            if (UScript.HasScript(0x0640, 0xaffe))
            {
                // An unguarded implementation might go into an infinite loop.
                Errln("UScript.hasScript(U+0640, bogus 0xaffe) is wrong");
            }
        }

        [Test]
        public void TestGetScriptExtensions()
        {
            BitSet scripts = new BitSet(UScript.CODE_LIMIT);

            /* invalid code points */
            if (UScript.GetScriptExtensions(-1, scripts) != UScript.UNKNOWN || scripts.Cardinality() != 1 ||
                    !scripts.Get(UScript.UNKNOWN))
            {
                Errln("UScript.getScriptExtensions(-1) is not {UNKNOWN}");
            }
            if (UScript.GetScriptExtensions(0x110000, scripts) != UScript.UNKNOWN || scripts.Cardinality() != 1 ||
                    !scripts.Get(UScript.UNKNOWN))
            {
                Errln("UScript.getScriptExtensions(0x110000) is not {UNKNOWN}");
            }

            /* normal usage */
            if (UScript.GetScriptExtensions(0x063f, scripts) != UScript.ARABIC || scripts.Cardinality() != 1 ||
                    !scripts.Get(UScript.ARABIC))
            {
                Errln("UScript.getScriptExtensions(U+063F) is not {ARABIC}");
            }
            if (UScript.GetScriptExtensions(0x0640, scripts) > -3 || scripts.Cardinality() < 3 ||
               !scripts.Get(UScript.ARABIC) || !scripts.Get(UScript.SYRIAC) || !scripts.Get(UScript.MANDAIC)
            )
            {
                Errln("UScript.getScriptExtensions(U+0640) failed");
            }
            if (UScript.GetScriptExtensions(0xfdf2, scripts) != -2 || scripts.Cardinality() != 2 ||
                    !scripts.Get(UScript.ARABIC) || !scripts.Get(UScript.THAANA))
            {
                Errln("UScript.getScriptExtensions(U+FDF2) failed");
            }
            if (UScript.GetScriptExtensions(0xff65, scripts) != -6 || scripts.Cardinality() != 6 ||
                    !scripts.Get(UScript.BOPOMOFO) || !scripts.Get(UScript.YI))
            {
                Errln("UScript.getScriptExtensions(U+FF65) failed");
            }
        }

        [Test]
        public void TestScriptMetadataAPI()
        {
            /* API & code coverage. */
            String sample = UScript.GetSampleString(UScript.LATIN);
            if (sample.Length != 1 || UScript.GetScript(sample[0]) != UScript.LATIN)
            {
                Errln("UScript.getSampleString(Latn) failed");
            }
            sample = UScript.GetSampleString(UScript.INVALID_CODE);
            if (sample.Length != 0)
            {
                Errln("UScript.getSampleString(invalid) failed");
            }

            if (UScript.GetUsage(UScript.LATIN) != ScriptUsage.RECOMMENDED ||
                    // Unicode 10 gives up on "aspirational".
                    UScript.GetUsage(UScript.YI) != ScriptUsage.LIMITED_USE ||
                    UScript.GetUsage(UScript.CHEROKEE) != ScriptUsage.LIMITED_USE ||
                    UScript.GetUsage(UScript.COPTIC) != ScriptUsage.EXCLUDED ||
                    UScript.GetUsage(UScript.CIRTH) != ScriptUsage.NOT_ENCODED ||
                    UScript.GetUsage(UScript.INVALID_CODE) != ScriptUsage.NOT_ENCODED ||
                    UScript.GetUsage(UScript.CODE_LIMIT) != ScriptUsage.NOT_ENCODED)
            {
                Errln("UScript.getUsage() failed");
            }

            if (UScript.IsRightToLeft(UScript.LATIN) ||
                    UScript.IsRightToLeft(UScript.CIRTH) ||
                    !UScript.IsRightToLeft(UScript.ARABIC) ||
                    !UScript.IsRightToLeft(UScript.HEBREW))
            {
                Errln("UScript.isRightToLeft() failed");
            }

            if (UScript.BreaksBetweenLetters(UScript.LATIN) ||
                    UScript.BreaksBetweenLetters(UScript.CIRTH) ||
                    !UScript.BreaksBetweenLetters(UScript.HAN) ||
                    !UScript.BreaksBetweenLetters(UScript.THAI))
            {
                Errln("UScript.breaksBetweenLetters() failed");
            }

            if (UScript.IsCased(UScript.CIRTH) ||
                    UScript.IsCased(UScript.HAN) ||
                    !UScript.IsCased(UScript.LATIN) ||
                    !UScript.IsCased(UScript.GREEK))
            {
                Errln("UScript.isCased() failed");
            }
        }

        /**
         * Maps a special script code to the most common script of its encoded characters.
         */
        private static int GetCharScript(int script)
        {
            switch (script)
            {
                case UScript.HAN_WITH_BOPOMOFO:
                case UScript.SIMPLIFIED_HAN:
                case UScript.TRADITIONAL_HAN:
                    return UScript.HAN;
                case UScript.JAPANESE:
                    return UScript.HIRAGANA;
                case UScript.JAMO:
                case UScript.KOREAN:
                    return UScript.HANGUL;
                case UScript.SYMBOLS_EMOJI:
                    return UScript.SYMBOLS;
                default:
                    return script;
            }
        }

        [Test]
        public void TestScriptMetadata()
        {
            UnicodeSet rtl = new UnicodeSet("[[:bc=R:][:bc=AL:]-[:Cn:]-[:sc=Common:]]");
            // So far, sample characters are uppercase.
            // Georgian is special.
            UnicodeSet cased = new UnicodeSet("[[:Lu:]-[:sc=Common:]-[:sc=Geor:]]");
            for (int sc = 0; sc < UScript.CODE_LIMIT; ++sc)
            {
                String sn = UScript.GetShortName(sc);
                ScriptUsage usage = UScript.GetUsage(sc);
                String sample = UScript.GetSampleString(sc);
                UnicodeSet scriptSet = new UnicodeSet();
                scriptSet.ApplyIntPropertyValue((int)UProperty.SCRIPT, sc); // ICU4N TODO: API - eliminate the cast ?
                if (usage == ScriptUsage.NOT_ENCODED)
                {
                    assertTrue(sn + " not encoded, no sample", sample.Length == 0);  // Java 6: sample.isEmpty()
                    assertFalse(sn + " not encoded, not RTL", UScript.IsRightToLeft(sc));
                    assertFalse(sn + " not encoded, not LB letters", UScript.BreaksBetweenLetters(sc));
                    assertFalse(sn + " not encoded, not cased", UScript.IsCased(sc));
                    assertTrue(sn + " not encoded, no characters", scriptSet.IsEmpty);
                }
                else
                {
                    assertFalse(sn + " encoded, has a sample character", sample.Length == 0);  // Java 6: sample.isEmpty()
                    int firstChar = sample.CodePointAt(0);
                    int charScript = GetCharScript(sc);
                    assertEquals(sn + " script(sample(script))",
                                 charScript, UScript.GetScript(firstChar));
                    assertEquals(sn + " RTL vs. set", rtl.Contains(firstChar), UScript.IsRightToLeft(sc));
                    assertEquals(sn + " cased vs. set", cased.Contains(firstChar), UScript.IsCased(sc));
                    assertEquals(sn + " encoded, has characters", sc == charScript, !scriptSet.IsEmpty);
                    if (UScript.IsRightToLeft(sc))
                    {
                        rtl.RemoveAll(scriptSet);
                    }
                    if (UScript.IsCased(sc))
                    {
                        cased.RemoveAll(scriptSet);
                    }
                }
            }
            assertEquals("no remaining RTL characters", "[]", rtl.ToPattern(true));
            assertEquals("no remaining cased characters", "[]", cased.ToPattern(true));

            assertTrue("Hani breaks between letters", UScript.BreaksBetweenLetters(UScript.HAN));
            assertTrue("Thai breaks between letters", UScript.BreaksBetweenLetters(UScript.THAI));
            assertFalse("Latn does not break between letters", UScript.BreaksBetweenLetters(UScript.LATIN));
        }

        [Test]
        public void TestScriptNames()
        {
            for (int i = 0; i < UScript.CODE_LIMIT; i++)
            {
                String name = UScript.GetName(i);
                if (name.Equals(""))
                {
                    Errln("FAILED: getName for code : " + i);
                }
                String shortName = UScript.GetShortName(i);
                if (shortName.Equals(""))
                {
                    Errln("FAILED: getName for code : " + i);
                }
            }
        }
        [Test]
        public void TestAllCodepoints()
        {
            int code;
            //String oldId="";
            //String oldAbbrId="";
            for (int i = 0; i <= 0x10ffff; i++)
            {
                code = UScript.INVALID_CODE;
                code = UScript.GetScript(i);
                if (code == UScript.INVALID_CODE)
                {
                    Errln("UScript.getScript for codepoint 0x" + Hex(i) + " failed");
                }
                String id = UScript.GetName(code);
                if (id.IndexOf("INVALID") >= 0)
                {
                    Errln("UScript.getScript for codepoint 0x" + Hex(i) + " failed");
                }
                String abbr = UScript.GetShortName(code);
                if (abbr.IndexOf("INV") >= 0)
                {
                    Errln("UScript.getScript for codepoint 0x" + Hex(i) + " failed");
                }
            }
        }
        [Test]
        public void TestNewCode()
        {
            /*
             * These script codes were originally added to ICU pre-3.6, so that ICU would
             * have all ISO 15924 script codes. ICU was then based on Unicode 4.1.
             * These script codes were added with only short names because we don't
             * want to invent long names ourselves.
             * Unicode 5 and later encode some of these scripts and give them long names.
             * Whenever this happens, the long script names here need to be updated.
             */
            String[] expectedLong = new String[]{
                "Balinese", "Batak", "Blis", "Brahmi", "Cham", "Cirt", "Cyrs",
                "Egyd", "Egyh", "Egyptian_Hieroglyphs",
                "Geok", "Hans", "Hant", "Pahawh_Hmong", "Old_Hungarian", "Inds",
                "Javanese", "Kayah_Li", "Latf", "Latg",
                "Lepcha", "Linear_A", "Mandaic", "Maya", "Meroitic_Hieroglyphs",
                "Nko", "Old_Turkic", "Old_Permic", "Phags_Pa", "Phoenician",
                "Miao", "Roro", "Sara", "Syre", "Syrj", "Syrn", "Teng", "Vai", "Visp", "Cuneiform",
                "Zxxx", "Unknown",
                "Carian", "Jpan", "Tai_Tham", "Lycian", "Lydian", "Ol_Chiki", "Rejang", "Saurashtra", "SignWriting", "Sundanese",
                "Moon", "Meetei_Mayek",
                /* new in ICU 4.0 */
                "Imperial_Aramaic", "Avestan", "Chakma", "Kore",
                "Kaithi", "Manichaean", "Inscriptional_Pahlavi", "Psalter_Pahlavi", "Phlv",
                "Inscriptional_Parthian", "Samaritan", "Tai_Viet",
                "Zmth", "Zsym",
                /* new in ICU 4.4 */
                "Bamum", "Lisu", "Nkgb", "Old_South_Arabian",
                /* new in ICU 4.6 */
                "Bassa_Vah", "Duployan", "Elbasan", "Grantha", "Kpel",
                "Loma", "Mende_Kikakui", "Meroitic_Cursive",
                "Old_North_Arabian", "Nabataean", "Palmyrene", "Khudawadi", "Warang_Citi",
                /* new in ICU 4.8 */
                "Afak", "Jurc", "Mro", "Nushu", "Sharada", "Sora_Sompeng", "Takri", "Tangut", "Wole",
                /* new in ICU 49 */
                "Anatolian_Hieroglyphs", "Khojki", "Tirhuta",
                /* new in ICU 52 */
                "Caucasian_Albanian", "Mahajani",
                /* new in ICU 54 */
                "Ahom", "Hatran", "Modi", "Multani", "Pau_Cin_Hau", "Siddham",
                // new in ICU 58
                "Adlam", "Bhaiksuki", "Marchen", "Newa", "Osage", "Hanb", "Jamo", "Zsye",
                // new in ICU 60
                "Masaram_Gondi", "Soyombo", "Zanabazar_Square"
            };
            String[] expectedShort = new String[]{
                "Bali", "Batk", "Blis", "Brah", "Cham", "Cirt", "Cyrs", "Egyd", "Egyh", "Egyp",
                "Geok", "Hans", "Hant", "Hmng", "Hung", "Inds", "Java", "Kali", "Latf", "Latg",
                "Lepc", "Lina", "Mand", "Maya", "Mero", "Nkoo", "Orkh", "Perm", "Phag", "Phnx",
                "Plrd", "Roro", "Sara", "Syre", "Syrj", "Syrn", "Teng", "Vaii", "Visp", "Xsux",
                "Zxxx", "Zzzz",
                "Cari", "Jpan", "Lana", "Lyci", "Lydi", "Olck", "Rjng", "Saur", "Sgnw", "Sund",
                "Moon", "Mtei",
                /* new in ICU 4.0 */
                "Armi", "Avst", "Cakm", "Kore",
                "Kthi", "Mani", "Phli", "Phlp", "Phlv", "Prti", "Samr", "Tavt",
                "Zmth", "Zsym",
                /* new in ICU 4.4 */
                "Bamu", "Lisu", "Nkgb", "Sarb",
                /* new in ICU 4.6 */
                "Bass", "Dupl", "Elba", "Gran", "Kpel", "Loma", "Mend", "Merc",
                "Narb", "Nbat", "Palm", "Sind", "Wara",
                /* new in ICU 4.8 */
                "Afak", "Jurc", "Mroo", "Nshu", "Shrd", "Sora", "Takr", "Tang", "Wole",
                /* new in ICU 49 */
                "Hluw", "Khoj", "Tirh",
                /* new in ICU 52 */
                "Aghb", "Mahj",
                /* new in ICU 54 */
                "Ahom", "Hatr", "Modi", "Mult", "Pauc", "Sidd",
                // new in ICU 58
                "Adlm", "Bhks", "Marc", "Newa", "Osge", "Hanb", "Jamo", "Zsye",
                // new in ICU 60
                "Gonm", "Soyo", "Zanb"
            };
            if (expectedLong.Length != (UScript.CODE_LIMIT - UScript.BALINESE))
            {
                Errln("need to add new script codes in lang.TestUScript.java!");
                return;
            }
            int j = 0;
            int i = 0;
            for (i = UScript.BALINESE; i < UScript.CODE_LIMIT; i++, j++)
            {
                String name = UScript.GetName(i);
                if (name == null || !name.Equals(expectedLong[j]))
                {
                    Errln("UScript.getName failed for code" + i + name + "!=" + expectedLong[j]);
                }
                name = UScript.GetShortName(i);
                if (name == null || !name.Equals(expectedShort[j]))
                {
                    Errln("UScript.getShortName failed for code" + i + name + "!=" + expectedShort[j]);
                }
            }
            for (i = 0; i < expectedLong.Length; i++)
            {
                int[] ret = UScript.GetCode(expectedShort[i]);
                if (ret.Length > 1)
                {
                    Errln("UScript.getCode did not return expected number of codes for script" + expectedShort[i] + ". EXPECTED: 1 GOT: " + ret.Length);
                }
                if (ret[0] != (UScript.BALINESE + i))
                {
                    Errln("UScript.getCode did not return expected code for script" + expectedShort[i] + ". EXPECTED: " + (UScript.BALINESE + i) + " GOT: %i\n" + ret[0]);
                }
            }
        }
    }
}
