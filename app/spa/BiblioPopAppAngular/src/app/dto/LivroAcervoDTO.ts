import { AutorDTO } from './AutorDTO';
import { EditoraDTO } from './EditoraDTO';
export class LivroAcervoDTO {
  public LivroId = 0;
  public Titulo = '';
  public Estante = '';
  public AnoPublicacao = 0;
  public Editora: EditoraDTO = new EditoraDTO();
  public Autoria = '';
  public Autores: Array<AutorDTO> = [];
}
