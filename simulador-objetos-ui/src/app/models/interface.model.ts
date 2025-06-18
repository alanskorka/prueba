import { InterfaceMethodModel } from './interface-method.model';

export interface InterfaceModel {
  id: number;
  name: string;
  namespaceId: number;
  methods: InterfaceMethodModel[];
  implementedBy: number[]; // IDs of classes that implement this interface
} 