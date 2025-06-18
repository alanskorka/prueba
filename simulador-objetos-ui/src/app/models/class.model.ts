import { AttributeModel } from './attribute.model';
import { MethodModel } from './method.model';
import { InterfaceModel } from './interface.model';

export interface ClassModel {
  id: number;
  name: string;
  namespaceId: number;
  attributes: AttributeModel[];
  methods: MethodModel[];
  implementedInterfaces: InterfaceModel[];
  isStatic: boolean;
  baseClassId?: number;
  baseClass?: ClassModel;
} 