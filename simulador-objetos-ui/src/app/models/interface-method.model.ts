import { ParamModel } from '../services/param.service';

export interface InterfaceMethodModel {
  id?: string;
  name: string;
  returnType: string;
  parameters: ParamModel[];
  interfaceId?: string;
} 