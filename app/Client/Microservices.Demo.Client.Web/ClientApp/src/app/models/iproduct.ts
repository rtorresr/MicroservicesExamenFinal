import { ICover } from "./icover";
import { IQuestion } from "./iquestion";

export interface IProduct {
  Code: string;
  Name: string;
  Image: string;
  Description: string;
  Covers: Array<ICover>;
  Questions: Array<IQuestion>;
  MaxNumberOfInsured: number;
}
