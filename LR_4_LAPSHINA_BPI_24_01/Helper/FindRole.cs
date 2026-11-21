namespace LR_4_LAPSHINA_BPI_24_01.Helper
{
    public class FindRole
    {
        int id;
        public FindRole(int id)
        {
            this.id = id;
        }
        public bool RolePredicate(Model.Role role)
        {
            return role.Id == id;
        }
    }
}