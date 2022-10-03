using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdollcopy : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    private ConfigurableJoint m_configurableJoint;
    Quaternion targetInitialRotation;
    void Start()
    {
        this.m_configurableJoint = this.GetComponent<ConfigurableJoint>();
        this.targetInitialRotation = this.targetLimb.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
       //ele precisa q sua posiçao e totacao geral sejam mudadas conforme sao mudadas as do modelo]
       //ele copia tudo,menos as operaçoes de posicao e rotacao que tem a ver com bake in pose
       //se essa opcao for tirada o modelo tb faria o msm q o ragdoll esta fazendo
       //ele precisa pegar os valores de rotacao geral que estao sendo aplicados no animator que
       //esta aplicado ao "perso" que por sua vez esta mais alto na hierarquia q os hips e eh por isso q ele n tá pegando
       //pq a operaçao de copiar rotação só começa a partir dos hips.
       //a rotaçãp dos hips no mpdelo sempre fica no entorno de 43 ela reflete esse ricochete q o ragdoll sempre leva ao tentar girar
       //no hip o target deve ser o perso, pois eh ele o objeto q realmente sofre alteraçoes de rotação, as rotações dos filhos se anulam
       //pq pra eles o pai sempre vai ser o centro
    }
    private void FixedUpdate()
    {
       
        this.m_configurableJoint.targetRotation = copyRotation();
    }
    private Quaternion copyRotation()
    {
        return Quaternion.Inverse(this.targetLimb.localRotation) * this.targetInitialRotation ;
    }
}
