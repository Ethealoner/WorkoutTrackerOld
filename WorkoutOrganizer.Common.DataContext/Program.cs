using WorkoutTracker.Common.DataContext;
using WorkoutTracker.Common.DataEntity;

static void ViewBasicTables()
{
    using (WorkoutDatabase db = new())
    {
        
        IQueryable<Exercise>? exercise = db.Exercises;

        if (exercise is null)
        {
            Console.WriteLine("No exercises found");
            return;
        }

        foreach(var exercise2 in exercise)
        {
            Console.WriteLine($"{exercise2.Name}, {exercise2.Id}, {exercise2.TypeOfExercise}");
        }
    }
}

ViewBasicTables();