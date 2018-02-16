import { AutorDTO } from './AutorDTO';
import { EditoraDTO } from './EditoraDTO';
export class LivroDTO {
  public LivroId = 0;
  public Titulo = '';
  public Estante = '';
  public AnoPublicacao = 0;
  public EditoraId = 0;
  public Editora: EditoraDTO = new EditoraDTO();
  public Autores: Array<AutorDTO> = [];
}
