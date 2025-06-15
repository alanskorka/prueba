export interface ClassModel {
  id: number;
  name: string;
  namespaceId: number;
  attributes: any[]; // Se definirá 'AttributeModel' luego
  methods: any[]; // Se definirá 'MethodModel' luego
} 