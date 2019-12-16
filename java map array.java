import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

public class Main {

    public static void main(String[] args) {
        int[] arr = new int[] {1,2,3};
        int[] arr2 = Arrays.stream(arr).map(x->x*2).toArray();
    }
}
