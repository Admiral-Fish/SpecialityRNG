﻿using System.Collections.Generic;

namespace GameCubeRNG.Objects
{
    public class NatureLock
    {
        private LockInfo[] lockInfo;
        private int forwardCounter, backCount, frontCount, shadowCount, x;
        private uint pid, pidOriginal, gender, genderLower, genderUpper, nature, psv, psvtemp;
        private ShadowType type;
        public List<uint> rand;
        private XdRngR reverse;
        private XdRng forward;

        public NatureLock(int lockNum)
        {
            lockInfo = natureLockListGales(lockNum);
            rand = new List<uint>();
            if (lockInfo != null)
            {
                backCount = lockInfo.Length;
                frontCount = backCount == 1 ? 0 : backCount - 2;
                shadowCount = backCount - 1;
                x = 0;
                if (backCount == 1)
                    getCurrLock();
            }
            reverse = new XdRngR(0);
            forward = new XdRng(0);
        }

        public void changeLockGales(int lockNum)
        {
            lockInfo = natureLockListGales(lockNum);
            if (lockInfo != null)
            {
                backCount = lockInfo.Length;
                frontCount = backCount == 1 ? 0 : backCount - 2;
                shadowCount = backCount - 1;
                x = 0;
                if (backCount == 1)
                    getCurrLock();
            }
        }

        public void changeLockColo(int lockNum)
        {
            lockInfo = natureLockListColo(lockNum);
            if (lockInfo != null)
            {
                backCount = lockInfo.Length;
                frontCount = backCount == 1 ? 0 : backCount - 2;
                shadowCount = backCount - 1;
                x = 0;
                if (backCount == 1)
                    getCurrLock();
            }
        }

        private List<LockInfo[]> galesInfo = new List<LockInfo[]>
        {
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 0, 126), new LockInfo(12, 127, 255) }, // Altaria
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(12, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255) }, // Arbok
            null, null, null, null, // Articuno, Baltoy 3, Baltoy 1, Baltoy 2
            new LockInfo[] { new LockInfo(12, 0, 255), new LockInfo(18, 0, 126), new LockInfo(0, 0, 255) }, // Banette
            null, // Beedrill
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 190) }, // Butterfree
            null, // Carvanha
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126) }, // Chansey
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 127, 255), new LockInfo(6, 0, 190) }, // Delcatty
            new LockInfo[] { new LockInfo(18, 0, 126) }, // Dodrio
            new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(12, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }, // Dragonite
            new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(6, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }, // Dugtrio
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(18, 0, 126), new LockInfo(12, 127, 255) }, // Duskull
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 63, 255) }, // Electabuzz
            null, // Exeggutor
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(0, 0, 126), new LockInfo(12, 127, 255) }, // Farfetch'd
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }, // Golduck
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(12, 127, 255) }, // Grimer
            new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }, // Growlithe
            new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }, // Gulpin 3
            new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }, // Gulpin 1
            new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }, // Gulpin 2
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }, // Hitmonchan
            new LockInfo[] { new LockInfo(24, 0, 62), new LockInfo(6, 0, 255), new LockInfo(12, 0, 126), new LockInfo(18, 127, 255) }, // Hitmonlee
            null, null, null, // Houndour 3, Houndour 1, Houndour 2
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 0, 126) }, // Hypno
            new LockInfo[] { new LockInfo(12, 0, 255), new LockInfo(18, 0, 126), new LockInfo(0, 0, 255) }, // Kangaskhan
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(500, 500, 500), new LockInfo(500, 500, 500), new LockInfo(6, 0, 126) }, // Lapras
            new LockInfo[] { new LockInfo(0, 0, 126) }, // Ledyba
            new LockInfo[] { new LockInfo(6, 0, 255), new LockInfo(24, 127, 255) }, // Lickitung
            null, // Lugia
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(0, 0, 126) }, // Lunatone
            new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 127, 255) }, // Marcargo
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(18, 191, 255), new LockInfo(18, 127, 255) }, // Magmar
            new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(0, 127, 255), new LockInfo(18, 0, 255) }, // Magneton
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 127, 255) }, // Makuhita
            new LockInfo[] { new LockInfo(6, 0, 126) }, // Manetric
            null, null, null, // Mareep 3, Mareep 1, Mareep 2
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(500, 500, 500), new LockInfo(500, 500, 500), new LockInfo(6, 0, 126) }, // Marowak
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 127, 255) }, // Mawile
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(0, 0, 126), new LockInfo(6, 63, 255) }, // Meowth
            null, // Moltres
            new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255), new LockInfo(18, 127, 255), new LockInfo(18, 127, 255) }, // Mr. Mime
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(0, 127, 255) }, // Natu
            new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }, // Nosepass
            new LockInfo[] { new LockInfo(24, 0, 126), new LockInfo(0, 0, 255), new LockInfo(6, 127, 255) }, // Numel
            new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }, // Paras
            new LockInfo[] { new LockInfo(18, 31, 255), new LockInfo(12, 127, 255) }, // Pidgeotto
            new LockInfo[] { new LockInfo(6, 127, 255) }, // Pineco
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(18, 191, 255), new LockInfo(18, 127, 255) }, // Pinsir
            new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(24, 127, 255), new LockInfo(18, 127, 255), new LockInfo(18, 127, 255) }, // Poliwrath
            new LockInfo[] { new LockInfo(12, 0, 126) }, // Poochyena
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126), new LockInfo(12, 0, 126), new LockInfo(18, 0, 126) }, // Primeape
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 0, 126), new LockInfo(0, 63, 255) }, // Ralts
            new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 127, 255) }, // Rapidash
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(500, 500, 500), new LockInfo(18, 0, 126) }, // Raticate
            null, // Rhydon
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 127, 255) }, // Roselia
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 127, 255) }, // Sableye
            new LockInfo[] { new LockInfo(6, 0, 126) }, // Salamence
            new LockInfo[] { new LockInfo(24, 127, 255), new LockInfo(6, 0, 126) }, // Scyther
            new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(0, 127, 255), new LockInfo(0, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) }, // Seedot 3
            new LockInfo[] { new LockInfo(12, 127, 255), new LockInfo(0, 127, 255), new LockInfo(18, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) }, // Seedot 1
            new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(0, 0, 126), new LockInfo(0, 0, 126), new LockInfo(24, 0, 126), new LockInfo(6, 127, 255) }, // Seedot 2
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(18, 127, 255), new LockInfo(6, 127, 255) }, // Seel
            null, // Shellder
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 0, 190) }, // Shroomish
            new LockInfo[] { new LockInfo(18, 0, 126), new LockInfo(6, 0, 126), new LockInfo(24, 63, 255) }, // Snorlax
            new LockInfo[] { new LockInfo(6, 0, 126) }, // Snorunt
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 0, 255) }, // Solrock
            new LockInfo[] { new LockInfo(6, 0, 126), new LockInfo(18, 127, 255) }, // Spearow
            new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }, // Spheal 3
            new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }, // Spheal 1
            new LockInfo[] { new LockInfo(0, 0, 255), new LockInfo(12, 0, 126), new LockInfo(24, 127, 255) }, // Spheal 2
            new LockInfo[] { new LockInfo(6, 127, 255), new LockInfo(12, 0, 126) }, // Spinarak
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(500, 500, 500), new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(24, 0, 255) }, // Starmie
            null, // Swellow
            new LockInfo[] { new LockInfo(0, 127, 255), new LockInfo(18, 0, 126) }, // Swinub
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(6, 127, 255), new LockInfo(12, 0, 190) }, // Tangela
            null, null, null, // Tauros, Teddiursa, Togepi
            new LockInfo[] { new LockInfo(12, 63, 255), new LockInfo(24, 0, 255), new LockInfo(18, 0, 126) }, // Venomoth
            new LockInfo[] { new LockInfo(12, 0, 126), new LockInfo(18, 127, 255), new LockInfo(0, 127, 255) }, // Voltorb
            new LockInfo[] { new LockInfo(18, 127, 255), new LockInfo(6, 0, 126), new LockInfo(0, 127, 255) }, // Vulpix
            new LockInfo[] { new LockInfo(12, 63, 255), new LockInfo(24, 0, 255), new LockInfo(18, 0, 126) }, // Weepinbell
            null, null // Zangoose, Zapdos
        };

        private List<LockInfo[]> coloInfo = new List<LockInfo[]>()
        {
            new LockInfo[] { new LockInfo(0, 0, 126), new LockInfo(24, 127, 255) } // Makuhita
        };

        private LockInfo[] natureLockListGales(int natureLockIndex)
        {
            switch (natureLockIndex)
            {
                case 0: //Altaria
                    type = ShadowType.SecondShadow;
                    break;
                case 1: //Arbok
                    type = ShadowType.FirstShadow;
                    break;
                case 6: //Banette
                    type = ShadowType.SecondShadow;
                    break;
                case 8: //Butterfree
                    type = ShadowType.SecondShadow;
                    break;
                case 10: //Chansey
                    type = ShadowType.SecondShadow;
                    break;
                case 11: //Delcatty
                    type = ShadowType.FirstShadow;
                    break;
                case 12: //Dodrio
                    type = ShadowType.SingleLock;
                    break;
                case 13: //Dragonite
                    type = ShadowType.FirstShadow;
                    break;
                case 14: //Dugtrio
                    type = ShadowType.FirstShadow;
                    break;
                case 15: //Duskull
                    type = ShadowType.FirstShadow;
                    break;
                case 16: //Electabuzz
                    type = ShadowType.FirstShadow;
                    break;
                case 18: //Farfetch'd  
                    type = ShadowType.FirstShadow;
                    break;
                case 19: //Golduck
                    type = ShadowType.FirstShadow;
                    break;
                case 20: //Grimer
                    type = ShadowType.FirstShadow;
                    break;
                case 21: //Growlithe
                    type = ShadowType.SecondShadow;
                    break;
                case 22: //Gulpin 3
                case 23: //Gulpin 1
                case 24: //Gulpin 2
                    type = ShadowType.FirstShadow;
                    break;
                case 25: //Hitmonchan
                    type = ShadowType.FirstShadow;
                    break;
                case 26: //Hitmonlee
                    type = ShadowType.FirstShadow;
                    break;
                case 30: //Hypno
                    type = ShadowType.SecondShadow;
                    break;
                case 31: //Kangaskhan
                    type = ShadowType.FirstShadow;
                    break;
                case 32: //Lapras
                    type = ShadowType.SecondShadow;
                    break;
                case 33: //Ledyba
                    type = ShadowType.SingleLock;
                    break;
                case 34: //Lickitung
                    type = ShadowType.FirstShadow;
                    break;
                case 36: //Lunatone
                    type = ShadowType.FirstShadow;
                    break;
                case 37: //Marcargo
                    type = ShadowType.SecondShadow;
                    break;
                case 38: //Magmar
                    type = ShadowType.FirstShadow;
                    break;
                case 39: //Magneton
                    type = ShadowType.FirstShadow;
                    break;
                case 40: //Makuhita
                    type = ShadowType.FirstShadow;
                    break;
                case 41: //Manectric
                    type = ShadowType.SingleLock;
                    break;
                case 45: //Marowak
                    type = ShadowType.FirstShadow;
                    break;
                case 46: //Mawile
                    type = ShadowType.FirstShadow;
                    break;
                case 47: //Meowth
                    type = ShadowType.FirstShadow;
                    break;
                case 49: //Mr. Mime
                    type = ShadowType.SecondShadow;
                    break;
                case 50: //Natu
                    type = ShadowType.FirstShadow;
                    break;
                case 51: //Nosepass
                    type = ShadowType.FirstShadow;
                    break;
                case 52: //Numel
                    type = ShadowType.FirstShadow;
                    break;
                case 53: //Paras
                    type = ShadowType.FirstShadow;
                    break;
                case 54: //Pidgeotto
                    type = ShadowType.FirstShadow;
                    break;
                case 55: //Pineco
                    type = ShadowType.SingleLock;
                    break;
                case 56: //Pinsir
                    type = ShadowType.SecondShadow;
                    break;
                case 57: //Poliwrath
                    type = ShadowType.FirstShadow;
                    break;
                case 58: //Poochyena
                    type = ShadowType.SingleLock;
                    break;
                case 59: //Primeape
                    type = ShadowType.FirstShadow;
                    break;
                case 60: //Ralts
                    type = ShadowType.FirstShadow;
                    break;
                case 61: //Rapidash
                    type = ShadowType.FirstShadow;
                    break;
                case 62: //Raticate
                    type = ShadowType.FirstShadow;
                    break;
                case 64: //Roselia
                    type = ShadowType.FirstShadow;
                    break;
                case 65: //Sableye
                    type = ShadowType.SecondShadow;
                    break;
                case 66: //Salamence
                    type = ShadowType.Salamence;
                    break;
                case 67: //Scyther
                    type = ShadowType.FirstShadow;
                    break;
                case 68: //Seedot 3
                    type = ShadowType.FirstShadow;
                    break;
                case 69: //Seedot 1
                    type = ShadowType.FirstShadow;
                    break;
                case 70: //Seedot 2
                    type = ShadowType.FirstShadow;
                    break;
                case 71: //Seel
                    type = ShadowType.FirstShadow;
                    break;
                case 73: //Shroomish
                    type = ShadowType.FirstShadow;
                    break;
                case 74: //Snorlax
                    type = ShadowType.SecondShadow;
                    break;
                case 75: //Snorunt
                    type = ShadowType.SingleLock;
                    break;
                case 76: //Solrock
                    type = ShadowType.FirstShadow;
                    break;
                case 77: //Spearow
                    type = ShadowType.FirstShadow;
                    break;
                case 78: //Spheal 3
                    type = ShadowType.FirstShadow;
                    break;
                case 79: //Spheal 1
                    type = ShadowType.FirstShadow;
                    break;
                case 80: //Spheal 2
                    type = ShadowType.FirstShadow;
                    break;
                case 81: //Spinarak
                    type = ShadowType.FirstShadow;
                    break;
                case 82: //Starmie
                    type = ShadowType.FirstShadow;
                    break;
                case 84: //Swinub
                    type = ShadowType.FirstShadow;
                    break;
                case 85: //Tangela
                    type = ShadowType.FirstShadow;
                    break;
                case 89: //Venomoth
                    type = ShadowType.FirstShadow;
                    break;
                case 90: //Voltorb
                    type = ShadowType.FirstShadow;
                    break;
                case 91: //Vulpix
                    type = ShadowType.FirstShadow;
                    break;
                case 92: //Weepinbell
                    type = ShadowType.SecondShadow;
                    break;
                case 2: //Articuno 
                case 3: //Baltoy 3
                case 4: //Baltoy 1
                case 5: //Baltoy 2
                case 7: //Beedrill
                case 9: //Carvanha
                case 17: //Exeggutor
                case 27: //Houndour 3
                case 28: //Houndour 1
                case 29: //Houndour 2
                case 35: //Lugia
                case 42: //Mareep 3
                case 43: //Mareep 1
                case 44: //Mareep 2
                case 48: //Moltres
                case 63: //Rhydon
                case 72: //Shellder
                case 83: //Swellow
                case 86: //Tauros
                case 87: //Teddiursa
                case 88: //Togepi
                case 93: //Zangoose
                default: //Zapdos 
                    type = ShadowType.NoLock;
                    break;
            }
            return galesInfo[natureLockIndex];
        }

        private LockInfo[] natureLockListColo(int natureLockIndex)
        {
            switch (natureLockIndex)
            {
                case 0: // Makuhita
                    type = ShadowType.FirstShadow;
                    break;
            }
            return coloInfo[natureLockIndex];
        }

        public bool singleNL(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber();

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool salamenceUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(8);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool salamenceSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(6);

            //Build PID
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool salamenceShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            //Check how many advances from shiny skip and build PID
            psv = getPSVReverse();
            psvtemp = getPSVReverse();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                psv = getPSVReverse();
            }

            reverse.GetNext32BitNumber(5);
            pid = getPIDReverse();

            //Backwards nature lock check
            gender = pid & 255;
            return (gender >= genderLower && gender <= genderUpper && pid % 25 == nature);
        }

        public bool firstShadow(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber();

            //Build temp pid first to not waste time populating if first backwards nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool secondShadowUnset(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(8);

            //Build temp pid first to not waste time populating if first nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool secondShadowSet(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(6);

            //Build temp pid first to not waste time populating if first nl fails
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public bool secondShadowShinySkip(uint seed)
        {
            reverse.Seed = seed;
            reverse.GetNext32BitNumber(1);

            //Check how many advances from shiny skip and build initial pid for first nl
            psv = getPSVReverse();
            psvtemp = getPSVReverse();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                psv = getPSVReverse();
            }

            reverse.GetNext32BitNumber(5);
            pidOriginal = getPIDReverse();

            //Backwards nature lock check
            gender = pidOriginal & 255;
            if (!(gender >= lockInfo[0].genderLower && gender <= lockInfo[0].genderUpper && pidOriginal % 25 == lockInfo[0].nature))
                return false;

            //Backwards nature lock check loop
            for (x = 1; x < backCount; x++)
            {
                reverse.GetNext32BitNumber(3);
                pid = getPIDReverse();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countBackTwo();
                }
            }

            forward.Seed = reverse.Seed;
            forward.GetNext32BitNumber();

            //Forwards nature lock check loop
            for (x = frontCount; x >= 0; x--)
            {
                forward.GetNext32BitNumber(3);
                pid = getPIDForward();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwo();
                }
            }

            return pidOriginal == pid;
        }

        public void shadowCheckerFirstShadow(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 3;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public void shadowCheckerSecondShadowSet(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 8;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public void shadowCheckerSecondShadowUnset(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 10;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public void shadowCheckerSecondShinySkip(out uint pid, out uint iv1, out uint iv2)
        {
            forwardCounter = 5;

            for (x = shadowCount; x >= 0; x--)
            {
                forwardCounter += 4;
                pid = getPIDShadow();
                getCurrLock();
                if (nature != 500)
                {
                    gender = pid & 255;
                    if (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature))
                        countForwardTwoShadow();
                }
            }

            forwardCounter += 6;
            psv = getPSVShadow();
            forwardCounter++;
            psvtemp = getPSVShadow();
            while (psv == psvtemp)
            {
                psvtemp = psv;
                forwardCounter++;
                psv = getPSVShadow();
            }

            forwardCounter += 3;
            iv1 = rand[forwardCounter++] >> 16;
            iv2 = rand[forwardCounter] >> 16;
            forwardCounter += 2;
            pid = getPIDShadow();
        }

        public ShadowType getType()
        {
            return type;
        }

        private uint getPIDReverse()
        {
            return reverse.GetNext16BitNumber() | (reverse.GetNext32BitNumber() & 0xFFFF0000);
        }

        private uint getPSVReverse()
        {
            return (reverse.GetNext16BitNumber() ^ reverse.GetNext16BitNumber()) >> 3;
        }

        private uint getPIDForward()
        {
            return (forward.GetNext32BitNumber() & 0xFFFF0000) | forward.GetNext16BitNumber();
        }

        private uint getPIDShadow()
        {
            return (rand[forwardCounter++] & 0xFFFF0000) | (rand[forwardCounter] >> 16);
        }

        private uint getPSVShadow()
        {
            return ((rand[forwardCounter++] >> 16) ^ (rand[forwardCounter] >> 16)) >> 3;
        }

        private void countBackTwo()
        {
            do
            {
                pid = getPIDReverse();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void countForwardTwo()
        {
            do
            {
                pid = getPIDForward();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void countForwardTwoShadow()
        {
            do
            {
                forwardCounter++;
                pid = getPIDShadow();
                gender = pid & 255;
            } while (!(gender >= genderLower && gender <= genderUpper && pid % 25 == nature));
        }

        private void getCurrLock()
        {
            nature = lockInfo[x].nature;
            genderLower = lockInfo[x].genderLower;
            genderUpper = lockInfo[x].genderUpper;
        }

        public enum ShadowType
        {
            NoLock,
            SingleLock,
            FirstShadow,
            Salamence,
            SecondShadow
        }
    }

    class LockInfo
    {
        public uint nature { get; set; }

        public uint genderLower { get; set; }

        public uint genderUpper { get; set; }

        public LockInfo(uint nature, uint genderLower, uint genderUpper)
        {
            this.nature = nature;
            this.genderLower = genderLower;
            this.genderUpper = genderUpper;
        }
    }
}