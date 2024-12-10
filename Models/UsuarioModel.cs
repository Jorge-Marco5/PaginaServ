namespace MvcMovie.Models;

public class UsuarioModel{

    public int Edad;
    public float Peso;
    public String Enfermedad;
    public String Ingredientes;

    //Contructor vacío
    public UsuarioModel() {
        this.Edad = 0;
        this.Peso = 0;
        this.Enfermedad = "";
        this.Ingredientes = "";
    }

    //GET
    public int getEdad() {
        return Edad;
    }

   public float getPeso() {
        return Peso;
    }
   public String getEnfermedad() {
        return Enfermedad;
    }

public String getIngredientes() {
        return Ingredientes;
    }

//SET
    public void setEdad(int Edad){
        this.Edad = Edad;
    }

    public void setPeso(float Peso){
        this.Peso = Peso;
    }

    public void setEnfermedad(String Enfermedad) {
        this.Enfermedad = Enfermedad;
    }
    public void setIngredientes(String Ingredientes) {
        this.Ingredientes = Ingredientes;
    }

}