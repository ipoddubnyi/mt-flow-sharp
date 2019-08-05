(sort : arr, n)
    //n = 10
    //arr = [4, 5, 8, 1, 5, 4, 3, 3, 2, 9]

    i = 0
    <i < n>
        j = 0
        <j < n - 1>
            <a[j] >= a[j+1]>
                tmp = arr[j]
                arr[j] = arr[j+1]
                arr[j+1] = tmp

            j = j + 1
        <-

        i = i + 1
    <-
