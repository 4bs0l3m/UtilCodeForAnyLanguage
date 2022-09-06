        let nums = [0, 4, 5, 3, 8, 9,6,2,7,1]
        function sortNums(_nArr) {
        for (let i = 1; i < _nArr.length; i++) {
        let currentN = _nArr[i];
        let prevN = _nArr[i - 1];
        let tmpN = prevN;
        if (currentN < prevN) {
            _nArr[i - 1] = _nArr[i];
            _nArr[i] = tmpN;
            _nArr = sortNums(_nArr)
        }
        }
            return _nArr
        }
        console.log(sortNums(nums))
